using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_Stats : MonoBehaviour
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


    int StatMax = 999;
    int StatMin = 0;
    public void StatsSafetyCheck()
    {
        if(Health.y < StatMin) { Health.y = StatMin; }
        if(Magic.y < StatMin) { Magic.y = StatMin; }
        if(Attack < StatMin) { Attack = StatMin; }
        if(Resonance < StatMin) { Resonance = StatMin; }
        if(Defence < StatMin) { Defence = StatMin; }
        if(Constitution < StatMin) { Constitution = StatMin; }
        if(Speed < StatMin) { Speed = StatMin; }
        if(Accuracy < StatMin) { Accuracy = StatMin; }
        if(Evasion < StatMin) { Evasion = StatMin; }

        if(Health.y > StatMax) { Health.y = StatMax; }
        if(Magic.y > StatMax) { Magic.y = StatMax; }
        if(Attack > StatMax) { Attack = StatMax; }
        if(Resonance > StatMax) { Resonance = StatMax; }
        if(Defence > StatMax) { Defence = StatMax; }
        if(Constitution > StatMax) { Constitution = StatMax; }
        if(Speed > StatMax) { Speed = StatMax; }
        if(Accuracy > StatMax) { Accuracy = StatMax; }
        if(Evasion > StatMax) { Evasion = StatMax; }
    }

    public int ReturnStatAverage()
    {
        int i = Health.y + Magic.y + Attack + Resonance +  Defence + Constitution + Speed + Accuracy + Evasion;

        i = i / 9;


        return i;

    }

    public i_Stats ReturnRandomisedStats(int SetNumber)
    {
        i_Stats returnedstats = new i_Stats();

        Vector2Int[,] matrix = buildmatrix() ;

        returnedstats.Health.y = Random.Range(matrix[SetNumber, 0].x,matrix[SetNumber,0].y);//0 links to health
        returnedstats.Health.x = returnedstats.Health.y;
        returnedstats.Magic.y = Random.Range(matrix[SetNumber, 1].x, matrix[SetNumber, 1].y);//1 links to magic.
        returnedstats.Magic.x = returnedstats.Magic.y;

        returnedstats.Attack = Random.Range(matrix[SetNumber, 2].x, matrix[SetNumber, 2].y);//2 links to attack

        returnedstats.Defence = Random.Range(matrix[SetNumber, 3].x, matrix[SetNumber, 3].y);//3 links to defence

        returnedstats.Resonance = Random.Range(matrix[SetNumber, 4].x, matrix[SetNumber, 4].y);//4 links to resonance

        returnedstats.Constitution = Random.Range(matrix[SetNumber, 5].x, matrix[SetNumber, 5].y);//5 links to constitution

        returnedstats.Evasion = Random.Range(matrix[SetNumber, 6].x, matrix[SetNumber, 6].y);//6 links to evasion

        returnedstats.Accuracy = Random.Range(matrix[SetNumber, 7].x, matrix[SetNumber, 7].y);//7 links to accuracy

        returnedstats.Speed = Random.Range(matrix[SetNumber, 8].x, matrix[SetNumber, 8].y);//8 links to speed


        return returnedstats;
    }//Builds stats.

    Vector2Int[,] buildmatrix()
    {
        Vector2Int[,] matrix = new Vector2Int[17,9];

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

            //Speed
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

        //B3
        {
            int i = 7;
            //Health
            matrix[i, 0].x = 168;
            matrix[i, 0].y = 170;

            //Mana
            matrix[i, 1].x = 162;
            matrix[i, 1].y = 171;

            //Attack
            matrix[i, 2].x = 158;
            matrix[i, 2].y = 165;

            //Defence
            matrix[i, 3].x = 151;
            matrix[i, 3].y = 168;

            //Resonance
            matrix[i, 4].x = 161;
            matrix[i, 4].y = 179;

            //Constitution
            matrix[i, 5].x = 161;
            matrix[i, 5].y = 165;

            //Evasion
            matrix[i, 6].x = 164;
            matrix[i, 6].y = 172;

            //Accuracy
            matrix[i, 7].x = 159;
            matrix[i, 7].y = 165;

            //Accuracy
            matrix[i, 8].x = 163;
            matrix[i, 8].y = 166;
        }

        //B4
        {
            int i = 8;
            //Health
            matrix[i, 0].x = 110;
            matrix[i, 0].y = 142;

            //Mana
            matrix[i, 1].x = 143;
            matrix[i, 1].y = 159;

            //Attack
            matrix[i, 2].x = 161;
            matrix[i, 2].y = 179;

            //Defence
            matrix[i, 3].x = 175;
            matrix[i, 3].y = 186;

            //Resonance
            matrix[i, 4].x = 141;
            matrix[i, 4].y = 168;

            //Constitution
            matrix[i, 5].x = 148;
            matrix[i, 5].y = 167;

            //Evasion
            matrix[i, 6].x = 164;
            matrix[i, 6].y = 172;

            //Accuracy
            matrix[i, 7].x = 168;
            matrix[i, 7].y = 171;

            //Accuracy
            matrix[i, 8].x = 168;
            matrix[i, 8].y = 171;
        }

        //C1
        {
            int i = 9;
            //Health
            matrix[i, 0].x = 58;
            matrix[i, 0].y = 68;

            //Mana
            matrix[i, 1].x = 78;
            matrix[i, 1].y = 90;

            //Attack
            matrix[i, 2].x = 68;
            matrix[i, 2].y = 75;

            //Defence
            matrix[i, 3].x = 61;
            matrix[i, 3].y = 81;

            //Resonance
            matrix[i, 4].x = 69;
            matrix[i, 4].y = 73;

            //Constitution
            matrix[i, 5].x = 68;
            matrix[i, 5].y = 71;

            //Evasion
            matrix[i, 6].x = 73;
            matrix[i, 6].y = 81;

            //Accuracy
            matrix[i, 7].x = 81;
            matrix[i, 7].y = 83;

            //Accuracy
            matrix[i, 8].x = 59;
            matrix[i, 8].y = 80;
        }

        //C2
        {
            int i = 10;
            //Health
            matrix[i, 0].x = 73;
            matrix[i, 0].y = 78;

            //Mana
            matrix[i, 1].x = 63;
            matrix[i, 1].y = 68;

            //Attack
            matrix[i, 2].x = 65;
            matrix[i, 2].y = 78;

            //Defence
            matrix[i, 3].x = 65;
            matrix[i, 3].y = 83;

            //Resonance
            matrix[i, 4].x = 63;
            matrix[i, 4].y = 83;

            //Constitution
            matrix[i, 5].x = 63;
            matrix[i, 5].y = 88;

            //Evasion
            matrix[i, 6].x = 79;
            matrix[i, 6].y = 83;

            //Accuracy
            matrix[i, 7].x = 59;
            matrix[i, 7].y = 62;

            //Accuracy
            matrix[i, 8].x = 59;
            matrix[i, 8].y = 88;
        }

        //C3
        {
            int i = 11;
            //Health
            matrix[i, 0].x = 68;
            matrix[i, 0].y = 70;

            //Mana
            matrix[i, 1].x = 62;
            matrix[i, 1].y = 71;

            //Attack
            matrix[i, 2].x = 58;
            matrix[i, 2].y = 65;

            //Defence
            matrix[i, 3].x = 51;
            matrix[i, 3].y = 68;

            //Resonance
            matrix[i, 4].x = 61;
            matrix[i, 4].y = 79;

            //Constitution
            matrix[i, 5].x = 61;
            matrix[i, 5].y = 65;

            //Evasion
            matrix[i, 6].x = 64;
            matrix[i, 6].y = 72;

            //Accuracy
            matrix[i, 7].x = 59;
            matrix[i, 7].y = 65;

            //Accuracy
            matrix[i, 8].x = 63;
            matrix[i, 8].y = 66;
        }

        //C4
        {
            int i = 12;
            //Health
            matrix[i, 0].x = 51;
            matrix[i, 0].y = 84;

            //Mana
            matrix[i, 1].x = 43;
            matrix[i, 1].y = 59;

            //Attack
            matrix[i, 2].x = 61;
            matrix[i, 2].y = 79;

            //Defence
            matrix[i, 3].x = 75;
            matrix[i, 3].y = 86;

            //Resonance
            matrix[i, 4].x = 41;
            matrix[i, 4].y = 68;

            //Constitution
            matrix[i, 5].x = 48;
            matrix[i, 5].y = 67;

            //Evasion
            matrix[i, 6].x = 64;
            matrix[i, 6].y = 72;

            //Accuracy
            matrix[i, 7].x = 68;
            matrix[i, 7].y = 71;

            //Accuracy
            matrix[i, 8].x = 68;
            matrix[i, 8].y = 71;
        }

        //D1
        {
            int i = 13;
            //Health
            matrix[i, 0].x = 12;
            matrix[i, 0].y = 28;

            //Mana
            matrix[i, 1].x = 38;
            matrix[i, 1].y = 50;

            //Attack
            matrix[i, 2].x = 18;
            matrix[i, 2].y = 25;

            //Defence
            matrix[i, 3].x = 16;
            matrix[i, 3].y = 21;

            //Resonance
            matrix[i, 4].x = 19;
            matrix[i, 4].y = 23;

            //Constitution
            matrix[i, 5].x = 28;
            matrix[i, 5].y = 31;

            //Evasion
            matrix[i, 6].x = 33;
            matrix[i, 6].y = 41;

            //Accuracy
            matrix[i, 7].x = 41;
            matrix[i, 7].y = 43;

            //Accuracy
            matrix[i, 8].x = 19;
            matrix[i, 8].y = 40;
        }

        //D2
        {
            int i = 14;
            //Health
            matrix[i, 0].x = 43;
            matrix[i, 0].y = 48;

            //Mana
            matrix[i, 1].x = 23;
            matrix[i, 1].y = 28;

            //Attack
            matrix[i, 2].x = 25;
            matrix[i, 2].y = 28;

            //Defence
            matrix[i, 3].x = 15;
            matrix[i, 3].y = 23;

            //Resonance
            matrix[i, 4].x = 23;
            matrix[i, 4].y = 56;

            //Constitution
            matrix[i, 5].x = 43;
            matrix[i, 5].y = 48;

            //Evasion
            matrix[i, 6].x = 19;
            matrix[i, 6].y = 43;

            //Accuracy
            matrix[i, 7].x = 19;
            matrix[i, 7].y = 22;

            //Accuracy
            matrix[i, 8].x = 19;
            matrix[i, 8].y = 48;
        }

        //D3
        {
            int i = 15;
            //Health
            matrix[i, 0].x = 38;
            matrix[i, 0].y = 50;

            //Mana
            matrix[i, 1].x = 32;
            matrix[i, 1].y = 41;

            //Attack
            matrix[i, 2].x = 18;
            matrix[i, 2].y = 25;

            //Defence
            matrix[i, 3].x = 21;
            matrix[i, 3].y = 28;

            //Resonance
            matrix[i, 4].x = 41;
            matrix[i, 4].y = 49;

            //Constitution
            matrix[i, 5].x = 31;
            matrix[i, 5].y = 35;

            //Evasion
            matrix[i, 6].x = 14;
            matrix[i, 6].y = 22;

            //Accuracy
            matrix[i, 7].x = 29;
            matrix[i, 7].y = 25;

            //Accuracy
            matrix[i, 8].x = 23;
            matrix[i, 8].y = 36;
        }

        //D4
        {
            int i = 16;
            //Health
            matrix[i, 0].x = 31;
            matrix[i, 0].y = 64;

            //Mana
            matrix[i, 1].x = 23;
            matrix[i, 1].y = 29;

            //Attack
            matrix[i, 2].x = 41;
            matrix[i, 2].y = 49;

            //Defence
            matrix[i, 3].x = 35;
            matrix[i, 3].y = 46;

            //Resonance
            matrix[i, 4].x = 21;
            matrix[i, 4].y = 28;

            //Constitution
            matrix[i, 5].x = 18;
            matrix[i, 5].y = 27;

            //Evasion
            matrix[i, 6].x = 24;
            matrix[i, 6].y = 32;

            //Accuracy
            matrix[i, 7].x = 28;
            matrix[i, 7].y = 31;

            //Accuracy
            matrix[i, 8].x = 18;
            matrix[i, 8].y = 41;
        }



        return matrix;
    }//Creates a complete matrix
}
