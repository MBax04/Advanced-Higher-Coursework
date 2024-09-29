using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyParentScript : MonoBehaviour
{
    // The enemies are stored as an array of objects
    public GameObject[] enemies;

    private float randomSpeed;
    private int swap1;
    private int swap2;
    private int[] uniqueNumArray;
    private int i;
    private int j;
    private int k;
    private int l;
    private Vector2[] spawnPositions;
    private int placeholder;
    private int level;

    private void Start()
    {
        // All methods are called immediately
        InstantiateSpawnPositions();
        SetSpeed();
        SetPosition();
    }

    private void InstantiateSpawnPositions()
    {
        // This Sets the place an enemy can randomly spawn to for each level
        level = SceneManager.GetActiveScene().buildIndex;
        if(level == 1)
        {
            spawnPositions = new Vector2[4];

            spawnPositions[0] = new Vector2(20, -0.45f);
            spawnPositions[1] = new Vector2(36, 6.55f);
            spawnPositions[2] = new Vector2(44, 6.55f);
            spawnPositions[3] = new Vector2(29, 2.55f);
        }
        else if (level == 2)
        {
            spawnPositions = new Vector2[4];

            spawnPositions[0] = new Vector2(15, -0.45f);
            spawnPositions[1] = new Vector2(25, -0.45f);
            spawnPositions[2] = new Vector2(66, 14.55f);
            spawnPositions[3] = new Vector2(77, 14.55f);
        }
        else if (level == 3)
        {
            spawnPositions = new Vector2[9];

            spawnPositions[0] = new Vector2(12, 3.55f);
            spawnPositions[1] = new Vector2(20, 3.55f);
            spawnPositions[2] = new Vector2(29, 3.55f);
            spawnPositions[3] = new Vector2(37, 5.55f);
            spawnPositions[4] = new Vector2(45, 7.55f);
            spawnPositions[5] = new Vector2(56, 8.55f);
            spawnPositions[6] = new Vector2(64, 9.55f);
            spawnPositions[7] = new Vector2(73, 11.55f);
            spawnPositions[8] = new Vector2(81, 15.55f);
        }
        else if (level == 4)
        {
            spawnPositions = new Vector2[28];

            spawnPositions[0] = new Vector2(-47, -0.45f);
            spawnPositions[1] = new Vector2(-42, -0.45f);
            spawnPositions[2] = new Vector2(-37, -0.45f);
            spawnPositions[3] = new Vector2(-32, -0.45f);
            spawnPositions[4] = new Vector2(-27, -0.45f);
            spawnPositions[5] = new Vector2(-22, -0.45f);
            spawnPositions[6] = new Vector2(-17, -0.45f);
            spawnPositions[7] = new Vector2(-12, -0.45f);
            spawnPositions[8] = new Vector2(-30, -24.45f);
            spawnPositions[9] = new Vector2(-25, -20.45f);
            spawnPositions[10] = new Vector2(-21, -16.45f);
            spawnPositions[11] = new Vector2(-15, -12.45f);
            spawnPositions[12] = new Vector2(-11, -8.45f);
            spawnPositions[13] = new Vector2(-14, 7.55f);
            spawnPositions[14] = new Vector2(-19, 11.55f);
            spawnPositions[15] = new Vector2(-24, 15.55f);
            spawnPositions[16] = new Vector2(-29, 19.55f);
            spawnPositions[17] = new Vector2(18, -0.45f);
            spawnPositions[18] = new Vector2(25, -0.45f);
            spawnPositions[19] = new Vector2(23, -13.45f);
            spawnPositions[20] = new Vector2(16, 7.55f);
            spawnPositions[21] = new Vector2(21, 3.55f);
            spawnPositions[22] = new Vector2(26, 7.55f);
            spawnPositions[23] = new Vector2(31, 3.55f);
            spawnPositions[24] = new Vector2(36, 7.55f);
            spawnPositions[25] = new Vector2(41, 3.55f);
            spawnPositions[26] = new Vector2(46, 7.55f);
            spawnPositions[27] = new Vector2(51, 3.55f);
        }
        else if (level == 5)
        {
            spawnPositions = new Vector2[10];

            spawnPositions[0] = new Vector2(7, 4.55f);
            spawnPositions[1] = new Vector2(-7, 7.55f);
            spawnPositions[2] = new Vector2(7, 10.55f);
            spawnPositions[3] = new Vector2(-7, 13.55f);
            spawnPositions[4] = new Vector2(7, 16.55f);
            spawnPositions[5] = new Vector2(7, 20.55f);
            spawnPositions[6] = new Vector2(-7, 23.55f);
            spawnPositions[7] = new Vector2(-7, 27.55f);
            spawnPositions[8] = new Vector2(7, 30.55f);
            spawnPositions[9] = new Vector2(7, 34.55f);
        }
        else if (level == 6)
        {
            spawnPositions = new Vector2[14];

            spawnPositions[0] = new Vector2(13, 4.55f);
            spawnPositions[1] = new Vector2(18, 8.55f);
            spawnPositions[2] = new Vector2(9, 12.55f);
            spawnPositions[3] = new Vector2(0, 15.55f);
            spawnPositions[4] = new Vector2(-12, 17.55f);
            spawnPositions[5] = new Vector2(-18, 21.55f);
            spawnPositions[6] = new Vector2(-9, 24.55f);
            spawnPositions[7] = new Vector2(4, 24.55f);
            spawnPositions[8] = new Vector2(10, 28.55f);
            spawnPositions[9] = new Vector2(23, 28.55f);
            spawnPositions[10] = new Vector2(27, 32.55f);
            spawnPositions[11] = new Vector2(20, 36.55f);
            spawnPositions[12] = new Vector2(10, 39.55f);
            spawnPositions[13] = new Vector2(-10, 38.55f);
        }
        else if (level == 7)
        {
            spawnPositions = new Vector2[3];

            spawnPositions[0] = new Vector2(13, 0.55f);
            spawnPositions[1] = new Vector2(23, 0.55f);
            spawnPositions[2] = new Vector2(36, 0.55f);
        }
        else if (level == 8)
        {
            spawnPositions = new Vector2[9];

            spawnPositions[0] = new Vector2(27, 12.55f);
            spawnPositions[1] = new Vector2(37, 12.55f);
            spawnPositions[2] = new Vector2(45, 12.55f);
            spawnPositions[3] = new Vector2(60, 4.55f);
            spawnPositions[4] = new Vector2(72, 4.55f);
            spawnPositions[5] = new Vector2(78, 5.55f);
            spawnPositions[6] = new Vector2(84, 6.55f);
            spawnPositions[7] = new Vector2(90, 7.55f);
            spawnPositions[8] = new Vector2(96, 8.55f);
        }
        else if (level == 9)
        {
            spawnPositions = new Vector2[3];

            spawnPositions[0] = new Vector2(6, 4.55f);
            spawnPositions[1] = new Vector2(10, 11.55f);
            spawnPositions[2] = new Vector2(14, 18.55f);
        }
        else if (level == 10)
        {
            spawnPositions = new Vector2[12];

            spawnPositions[0] = new Vector2(17, 2.55f);
            spawnPositions[1] = new Vector2(26, 2.55f);
            spawnPositions[2] = new Vector2(34, 2.55f);
            spawnPositions[3] = new Vector2(42, 2.55f);
            spawnPositions[4] = new Vector2(40, 10.55f);
            spawnPositions[5] = new Vector2(33, 14.55f);
            spawnPositions[6] = new Vector2(25, 17.55f);
            spawnPositions[7] = new Vector2(17, 21.55f);
            spawnPositions[8] = new Vector2(7, 21.55f);
            spawnPositions[9] = new Vector2(-18, 10.55f);
            spawnPositions[10] = new Vector2(-22, 11.55f);
            spawnPositions[11] = new Vector2(-26, 12.55f);
        }
    }

    private void SetSpeed()
    {
        // This sets a random speed between a range to every enemy
        for (i = 0; i < enemies.Length; i++)
        {
            randomSpeed = Random.Range(1.0f, 3.0f);
            enemies[i].GetComponent<EnemyScript>().SetSpeed(randomSpeed);
        }
    }

    private void SetPosition()
    {
        uniqueNumArray = new int[spawnPositions.Length];

        // This generates a list of 1 to the amount of enemies
        for (k = 0; k < spawnPositions.Length; k++)
        {
            uniqueNumArray[k] = k;
        }   

        // This then shuffles that list so that the order is random
        for (j = 0; j < 30; j++)
        {
            swap1 = Random.Range(0, spawnPositions.Length);
            swap2 = Random.Range(0, spawnPositions.Length);

            placeholder = uniqueNumArray[swap1];
            uniqueNumArray[swap1] = uniqueNumArray[swap2];
            uniqueNumArray[swap2] = placeholder;
        }

        // The shuffled list is then used as a position in the array of spawn positions
        // So that where each eney spawns is random every time
        for (l = 0; l < enemies.Length; l++)
        {
            enemies[l].transform.position = spawnPositions[uniqueNumArray[l]];
        }
    }
}
