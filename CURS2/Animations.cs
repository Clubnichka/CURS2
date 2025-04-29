using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURS2
{
    public static class Animations
    {
        public static bool bedJump = false;
        public static bool tablePlateGlassJump = false;

        public static bool bedJumpActive = false;
        public static float bedJumpTime = 0f;
        public static float bedJumpOffset = 0f;

        public static bool tablePlateGlassJumpActive = false;
        public static float tableJumpTime = 0f;
        public static float tableJumpOffset = 0f;
        public static float plateJumpOffset = 0f;
        public static float glassJumpOffset = 0f;

        public static float jumpHeight = 0.2f; 
        public static float jumpSpeed = 0.005f;  


        public static float time = 0.0f;

        public static bool isBedJumping = false;

        public static bool isTableJumping = false;

        public static void UpdateBedJump()
        {
            if (isBedJumping)
            {
                bedJumpTime += jumpSpeed; 
                bedJumpOffset = (float)(Math.Abs(Math.Sin(bedJumpTime)) * jumpHeight);

                if (bedJumpTime >= Math.PI) 
                {
                    isBedJumping = false;
                    bedJumpOffset = 0f;
                    bedJumpTime = 0f;
                }
            }
        }

        public static void UpdateTableJump()
        {
            if (isTableJumping)
            {
                tableJumpTime += jumpSpeed; 
                tableJumpOffset = (float)(Math.Abs(Math.Sin(tableJumpTime)) * jumpHeight);
                plateJumpOffset = (float)(Math.Abs(Math.Sin(tableJumpTime * 1.1f)) * jumpHeight);
                glassJumpOffset = (float)(Math.Abs(Math.Sin(tableJumpTime * 1.2f)) * jumpHeight);

                if (tableJumpTime >= Math.PI) 
                {
                    isTableJumping = false;
                    tableJumpOffset = 0f;
                    plateJumpOffset = 0f;
                    glassJumpOffset = 0f;
                    tableJumpTime = 0f;
                }
            }
        }
    }
}
