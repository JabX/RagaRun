namespace RagaRun

open Drawing
open Microsoft.Xna.Framework
open Utils

type SlabSize = Narrow | Wide

type Slab = Position * SlabSize

type Turn = TurnLeft | Straight | TurnRight

type SlabLine = Slab list * Turn * float32


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SlabLine =
    open Config.Slab
    open Position

    let private rand = new System.Random ()

    let private newSlabSize position =
        match position with
        | ELeft | MLeft | MRight | ERight -> Narrow
        | _ ->  match rand.Next 4 with | 0 -> Narrow | _ -> Wide

    let private newSlabLine slabs turn =
        slabs
        |> List.head
        |> fun (p1, _) -> p1, newSlabSize p1 
        |> fun slab ->
            match slab with
            | Left, Narrow | Middle, Narrow | Right, Narrow -> [], Straight
            | (p, Narrow) -> match rand.Next 3 with
                             | 0 when p <> ELeft && turn <> TurnRight -> [goLeft p false, Wide], TurnLeft
                             | 1 when p <> ERight && turn <> TurnLeft -> [goRight p false, Wide], TurnRight
                             | _ -> [], Straight
            | (p, Wide) -> match rand.Next 6 with
                           | 0 when p <> Left && turn <> TurnRight -> [goLeft p false, Wide; goLeft p true, Narrow], TurnLeft
                           | 1 when p <> Right && turn <> TurnLeft -> [goRight p false, Wide; goRight p true, Narrow], TurnRight
                           | 2 when turn <> TurnRight -> [goLeft p true, Narrow], TurnLeft
                           | 3 when turn <> TurnLeft-> [goRight p true, Narrow], TurnRight
                           | _ -> [], Straight
            |> fun (positions, newTurn) -> List.append positions [slab], newTurn, -h

    let update slabLines (speed: float32) =
        let newSlabLines = 
            slabLines
            |> List.head
            |> fun (_, _, y) -> if y > Config.gameH + h then List.tail slabLines else slabLines
        newSlabLines
        |> List.rev
        |> List.head
        |> fun (slabLine, turn, y) -> if y > 0.0f then List.append newSlabLines [newSlabLine slabLine turn] else newSlabLines
        |> List.map (fun (slabs, turn, y) -> slabs, turn, y + speed)

    let getRect (p, s, y) =
        match s with | Narrow -> nw | Wide -> ww
        |> fun w -> new Rectangle (int <| Position.getX p - w / 2.0f, int <| y - h / 2.0f, int w, int h)

    let draw (slabLine, _, y) =
        slabLine |> List.iter (fun slab ->
            DrawingHelper.DrawRectangle (slab ++ y |> getRect, Color.DarkSlateGray, true))
