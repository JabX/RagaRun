namespace RagaRun

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

/// <summary>
/// Main game class, basic boilerplate for MonoGame.
/// </summary>
type RagaRun() as this =
    inherit Game()

    do this.Window.Title <- "RagaRun"

    // Initialisation of the GraphicsDeviceManager
    let graphics = new GraphicsDeviceManager (this)
    do graphics.PreferredBackBufferWidth <- int Config.gameW
    do graphics.PreferredBackBufferHeight <- int Config.gameH
    do graphics.PreferMultiSampling <- true

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable font = Unchecked.defaultof<SpriteFont>

    // Represents the keyboard state of the previous frame.
    let mutable lastKeyState = Keyboard.GetState ()

    // The state of the application. It's only that.
    let mutable world = World.initialWorld

    /// <summary>
    /// Load the content used by the game. Here only the spriteBatch and the empty texture.
    /// </summary>
    override this.LoadContent () =
        Drawing.DrawingHelper.Initialize(this.GraphicsDevice)
        spriteBatch <- new SpriteBatch (this.GraphicsDevice)
        font <- this.Content.Load<SpriteFont> "Assets/font"

    /// <summary>
    /// Updates the world.
    /// </summary>
    /// <param name="time">The GameTime object of the game.</param>
    override this.Update time =
        let keyState = Keyboard.GetState ()
        world <- World.update world keyState lastKeyState
        lastKeyState <- keyState

    /// <summary>
    /// Draws the world.
    /// </summary>
    /// <param name="time">The GameTime object of the game.</param>
    override this.Draw time =
        this.GraphicsDevice.Clear Color.Black
        spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied)
        World.draw world spriteBatch font
        spriteBatch.End ()

module Program =
    /// <summary>
    /// The entry point of the game. It simply runs the game described above.
    /// </summary>
    [<EntryPoint>]
    let main _ = 
        use g = new RagaRun()
        g.Run()
        0
