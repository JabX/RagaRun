namespace RagaRun

open Drawing
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

// float32 in (0, 1) represents the progression. Meaning that Small 0 = Big 1.
type Size = 
    | Small of float32
    | Big of float32

// (new position, old position, progression)
type TruePosition = Position * Position * float32

type Agar = TruePosition * Size

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Agar =
    open Config.Agar

    /// <summary>Determines if the size is actually small.</summary>
    let private isSmall s =
        match s with 
        | Small _  -> true 
        | Big _  -> false
    
    /// <summary>Moves the agar in the given direction.</summary>
    let private moveLR direction ((np, op, pc), s) =
        isSmall s |> fun isSmall ->
            match direction with
            | Keys.Left -> Position.goLeft np isSmall, np, 0.0f
            | Keys.Right -> Position.goRight np isSmall, np, 0.0f
            | _ -> np, op, pc
            |> fun tp -> tp, s

    /// <summary>Gets the true x position of the agar.</summary>
    let private getTrueX (np, op, pc) =
        pc * Position.getX np + (1.0f - pc) * Position.getX op

    /// <summary>Gets the radius of the size.</summary>
    let private getR s =
        match s with
        | Small r -> maxR - r * (maxR - minR)
        | Big r -> minR + r * (maxR - minR)
    
    /// <summary>Change the position in case it doesn't match with the current size.</summary>
    let private replace (np, op, pc) isSmall =
        match np with
        | ELeft when not isSmall -> Left, np, 0.0f
        | ERight when not isSmall -> Right, np, 0.0f
        | MLeft when not isSmall -> Middle, np, 0.0f
        | MRight when not isSmall -> Middle, np, 0.0f
        | np -> np, op, if pc < 1.0f then pc + prSp else 1.0f
    

    ///<summary>Changes the size of the agar in accordance with the keyboard states (invert progression).</summary>
    let resize (keys: KeyboardState) (lastKeys: KeyboardState) (tp, s) =
        match s with | Small r -> r | Big r -> r 
        |> fun r ->
            if keys.IsKeyDown Keys.Up && lastKeys.IsKeyUp Keys.Up then tp, Small (1.0f - r)
            else if keys.IsKeyUp Keys.Up && lastKeys.IsKeyDown Keys.Up then tp, Big (1.0f - r)
            else tp, s
    
    ///<summary>Updates the agar in accordance with the progressions.</summary>
    let update (tp, s) =
        match s with | Small r -> r | Big r -> r
        |> fun r -> if r < 1.0f then r + prSp else 1.0f
        |> fun r -> match s with
                    | Small _ -> Small r
                    | Big _ -> Big r
        |> fun r -> replace tp (isSmall r), r

    ///<summary>Moves the agar in accordance with the pressed keys.</summary>
    let move (newKeys: Set<Keys>) agar =
        if newKeys.Count = 0 then agar
        else 
            match newKeys |> Set.toList |> List.head with
            | Keys.Left -> moveLR Keys.Left agar
            | Keys.Right -> moveLR Keys.Right agar
            | _ -> agar
    
    ///<summary>Gets the agar speed.</summary>
    let getSpeed s =
        s |> getR |> fun rd -> minS + (maxR - rd) / (maxR - minR) * (maxS - minS)
    
    ///<summary>Draws the agar on the screen.</summary>
    let draw (tp, s) =
        DrawingHelper.DrawCircle (new Vector2 (getTrueX tp, y), getR s, Color.DarkSeaGreen, true)

    