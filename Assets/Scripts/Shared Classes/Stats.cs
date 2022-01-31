using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public Vector2Int Health;
    public Vector2Int Magic;
    public int Attack;
    public int Resonance;
    public int Defence;
    public int Constitution;
    public int Speed;
    public int Accuracy;
    public int Evasion;
   
    public Stats ReturnRandomisedStats(int SetNumber)
    {
        Stats returnedstats = new Stats();

        Vector2Int[,] matrix = buildmatrix() ;




        return returnedstats;
    }

    Vector2Int[,] buildmatrix()
    {
        Vector2Int[,] matrix = new Vector2Int[9,16];

        //A1
        {
            int i = 1;
            //Health
            matrix[i, 0].x = 358;
            matrix[i, 0].y = 368;

            //Mana
            matrix[i, 1].x = 378;
            matrix[i, 1].y = 390;

            //Attack
            matrix[i, 2].x = 368;
            matrix[i, 2].y = 375;

            //Defence
            matrix[i, 3].x = 361;
            matrix[i, 3].y = 381;

            //Resonance
            matrix[i, 4].x = 369;
            matrix[i, 4].y = 373;

            //Constitution
            matrix[i, 5].x = 369;
            matrix[i, 5].y = 381;

            //Evasion
            matrix[i, 6].x = 373;
            matrix[i, 6].y = 383;

            //Accuracy
            matrix[i, 7].x = 383;
            matrix[i, 7].y = 393;

            //Accuracy
            matrix[i, 8].x = 359;
            matrix[i, 8].y = 380;
        }

        //A2
        {
            int i = 2;
            //Health
            matrix[i, 0].x = 274;
            matrix[i, 0].y = 278;

            //Mana
            matrix[i, 1].x = 261;
            matrix[i, 1].y = 263;

            //Attack
            matrix[i, 2].x = 254;
            matrix[i, 2].y = 264;

            //Defence
            matrix[i, 3].x = 269;
            matrix[i, 3].y = 283;

            //Resonance
            matrix[i, 4].x = 263;
            matrix[i, 4].y = 283;

            //Constitution
            matrix[i, 5].x = 264;
            matrix[i, 5].y = 291;

            //Evasion
            matrix[i, 6].x = 211;
            matrix[i, 6].y = 245;

            //Accuracy
            matrix[i, 7].x = 286;
            matrix[i, 7].y = 291;

            //Accuracy
            matrix[i, 8].x = 259;
            matrix[i, 8].y = 288;
        }

        //A3
        {
            int i = 3;
            //Health
            matrix[i, 0].x = 268;
            matrix[i, 0].y = 280;

            //Mana
            matrix[i, 1].x = 262;
            matrix[i, 1].y = 271;

            //Attack
            matrix[i, 2].x = 258;
            matrix[i, 2].y = 265;

            //Defence
            matrix[i, 3].x = 251;
            matrix[i, 3].y = 268;

            //Resonance
            matrix[i, 4].x = 262;
            matrix[i, 4].y = 279;

            //Constitution
            matrix[i, 5].x = 261;
            matrix[i, 5].y = 265;

            //Evasion
            matrix[i, 6].x = 252;
            matrix[i, 6].y = 272;

            //Accuracy
            matrix[i, 7].x = 259;
            matrix[i, 7].y = 265;

            //Accuracy
            matrix[i, 8].x = 263;
            matrix[i, 8].y = 273;
        }

        //A4
        {
            int i = 4;
            //Health
            matrix[i, 0].x = 210;
            matrix[i, 0].y = 242;

            //Mana
            matrix[i, 1].x = 243;
            matrix[i, 1].y = 259;

            //Attack
            matrix[i, 2].x = 261;
            matrix[i, 2].y = 279;

            //Defence
            matrix[i, 3].x = 275;
            matrix[i, 3].y = 286;

            //Resonance
            matrix[i, 4].x = 241;
            matrix[i, 4].y = 270;

            //Constitution
            matrix[i, 5].x = 258;
            matrix[i, 5].y = 267;

            //Evasion
            matrix[i, 6].x = 264;
            matrix[i, 6].y = 272;

            //Accuracy
            matrix[i, 7].x = 268;
            matrix[i, 7].y = 271;

            //Accuracy
            matrix[i, 8].x = 268;
            matrix[i, 8].y = 278;
        }

        //B1
        {
            int i = 5;
            //Health
            matrix[i, 0].x = 158;
            matrix[i, 0].y = 168;

            //Mana
            matrix[i, 1].x = 178;
            matrix[i, 1].y = 190;

            //Attack
            matrix[i, 2].x = 168;
            matrix[i, 2].y = 175;

            //Defence
            matrix[i, 3].x = 161;
            matrix[i, 3].y = 181;

            //Resonance
            matrix[i, 4].x = 169;
            matrix[i, 4].y = 173;

            //Constitution
            matrix[i, 5].x = 168;
            matrix[i, 5].y = 171;

            //Evasion
            matrix[i, 6].x = 173;
            matrix[i, 6].y = 181;

            //Accuracy
            matrix[i, 7].x = 181;
            matrix[i, 7].y = 183;

            //Accuracy
            matrix[i, 8].x = 159;
            matrix[i, 8].y = 180;
        }

        //B2
        {
            int i = 6;
            //Health
            matrix[i, 0].x = 173;
            matrix[i, 0].y = 178;

            //Mana
            matrix[i, 1].x = 163;
            matrix[i, 1].y = 168;

            //Attack
            matrix[i, 2].x = 165;
            matrix[i, 2].y = 178;

            //Defence
            matrix[i, 3].x = 165;
            matrix[i, 3].y = 183;

            //Resonance
            matrix[i, 4].x = 163;
            matrix[i, 4].y = 183;

            //Constitution
            matrix[i, 5].x = 163;
            matrix[i, 5].y = 188;

            //Evasion
            matrix[i, 6].x = 179;
            matrix[i, 6].y = 183;

            //Accuracy
            matrix[i, 7].x = 159;
            matrix[i, 7].y = 162;

            //Accuracy
            matrix[i, 8].x = 159;
            matrix[i, 8].y = 188;
        }



        return matrix;
    }
}
