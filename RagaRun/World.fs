namespace RagaRun

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type World = Agar * SlabLine list

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module World =
    let initialWorld: World = ((Middle, Middle, 1.0f), Big 1.0f), [[Middle, Wide], Straight, 0.0f]

    let update (agar, slabLines) (keyState: KeyboardState) (lastKeyState: KeyboardState) =
        let newKeys = Set.difference (keyState.GetPressedKeys () |> Set.ofArray) (lastKeyState.GetPressedKeys () |> Set.ofArray)
        Agar.resize keyState lastKeyState agar
        |> Agar.move newKeys
        |> Agar.update
        |> fun agar -> agar, slabLines
        |> fun ((tp, s), slabLines) -> (tp, s), SlabLine.update slabLines (Agar.getSpeed s)

    let draw (agar, slabLines) (spriteBatch: SpriteBatch) font =
        slabLines |> List.iter SlabLine.draw 
        Agar.draw agar