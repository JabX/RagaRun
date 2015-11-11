namespace RagaRun

type Position = ELeft | Left | MLeft | Middle | MRight | Right | ERight

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Position =
    open Config

    let goLeft p isSmall =
        match p with
        | ELeft -> ELeft
        | Left when isSmall -> ELeft
        | Left | MLeft -> Left
        | Middle when isSmall -> MLeft
        | Middle -> Left
        | Right when isSmall -> MRight
        | Right | MRight  -> Middle
        | ERight -> Right

    let goRight p isSmall =
        match p with
        | ERight -> ERight
        | Right when isSmall -> ERight
        | Right | MRight -> Right
        | Middle when isSmall -> MRight
        | Middle -> Right
        | Left when isSmall -> MLeft
        | Left | MLeft  -> Middle
        | ELeft -> Left

    let getX p =
        match p with
        | ELeft -> 0.125f * gameW
        | Left -> 0.25f * gameW
        | MLeft -> 0.375f * gameW
        | Middle -> 0.5f * gameW
        | MRight -> 0.625f * gameW
        | Right -> 0.75f * gameW
        | ERight -> 0.875f * gameW