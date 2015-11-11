namespace RagaRun

module Utils =
    ///<summary>(a, b) ++ c = a, b, c</summary>
    let inline (++) (a, b) c = a, b, c