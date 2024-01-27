using UnityEngine;

namespace DTerrain
{
    /// <summary>
    /// Example script: Spawining Unity gameobject with collider to show that DTerrain works with Unity Colliders 2D.
    /// Press B to spawn a ball.
    /// </summary>
    public class MouseBallSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject ball = null;

        [SerializeField]
        private GameObject bomb = null;

        [SerializeField]
        private BasicPaintableLayer primaryLayer;
        [SerializeField]
        private BasicPaintableLayer secondaryLayer;

        void Update()
        {
            CreateBall();
        }

        public void CreateBall()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mPos.z = 0;
                GameObject newBomb = Instantiate(bomb, mPos, new Quaternion(0, 0, 0, 0));

                newBomb.GetComponent<TerrainBreaker>().primaryLayer = primaryLayer;
                newBomb.GetComponent<TerrainBreaker>().secondaryLayer = secondaryLayer;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mPos.z = 0;
                Instantiate(ball, mPos, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}