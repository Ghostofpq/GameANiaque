using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BunbyX
{
    class BunbyX:Sprite
    {
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int BASE_SPEED = 300;
        const string BUNNY_ASSETNAME = "lapin_test";
        enum State
        {
            Waiting,
            Running,
            Jumping
        }
        int Bunny_Speed_coef;
        int Bunny_Jump_coef;

        State Bunny_Current_State;
        Vector2 Bunny_Direction;
        Vector2 Bunny_Speed;

        KeyboardState mPreviousKeyboardState;
        float Scale;

        public BunbyX(int Start_Position_X,int Start_Position_Y)
        {
            Position = new Vector2(Start_Position_X, Start_Position_Y);
            Bunny_Speed_coef = 1;
            Bunny_Jump_coef = 1;
            Bunny_Current_State = State.Waiting;

            Bunny_Direction = Vector2.Zero;
            Bunny_Speed = Vector2.Zero;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, BUNNY_ASSETNAME);
            Source = new Rectangle(0, 0, Source.Width, Source.Height);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();

            UpdateMovement(aCurrentKeyboardState);
            UpdateJump();
            mPreviousKeyboardState = aCurrentKeyboardState;

            base.Update(theGameTime, Bunny_Speed, Bunny_Direction);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (Bunny_Current_State == State.Waiting || Bunny_Current_State == State.Running)
            {
                Bunny_Speed = Vector2.Zero;
                Bunny_Direction = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    Bunny_Speed.X = Bunny_Speed_coef * BASE_SPEED;
                    Bunny_Direction.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    Bunny_Speed.X = Bunny_Speed_coef * BASE_SPEED;
                    Bunny_Direction.X = MOVE_RIGHT;
                }
                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true && mPreviousKeyboardState.IsKeyDown(Keys.Up) == false)
                {
                    Bunny_Current_State = State.Jumping;
                    Bunny_Direction.Y = MOVE_UP;
                    Bunny_Speed = new Vector2(Bunny_Speed.X, 500);
                }
                //if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                //{
                //    mSpeed.Y = WIZARD_SPEED;
                //    mDirection.Y = MOVE_UP;
                //}
                //else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                //{
                //    mSpeed.Y = WIZARD_SPEED;
                //    mDirection.Y = MOVE_DOWN;
                //}
            }

        }
        private void UpdateJump()
        {
            if (Bunny_Current_State == State.Jumping)
            {
                if (Bunny_Direction.Y == MOVE_UP)
                {
                    Bunny_Speed = new Vector2(Bunny_Speed.X, Bunny_Speed.Y - 10);
                }
            }
        }

    }
}
