using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using EventHandler = Utilities.EventHandler;

namespace UI
{
    /// <summary>
    /// GameManager类负责管理游戏的分数和排行榜数据持久化。
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        /// <summary>
        /// 存储游戏分数的列表。
        /// </summary>
        public List<int> scoreList;
        /// <summary>
        /// 当前游戏得分。
        /// </summary>
        private int score;
        /// <summary>
        /// 数据存储路径。
        /// </summary>
        private string dataPath;

        /// <summary>
        /// 在游戏对象唤醒时调用，初始化数据路径并确保游戏对象在场景切换时不被销毁。
        /// </summary>
        private void Awake()
        {
            dataPath = Application.persistentDataPath + "/leaderboard.json";
            scoreList= GetScoreListData();
            if (instance==null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this);
            
        }

        /// <summary>
        /// 在组件启用时调用，注册事件处理函数。
        /// </summary>
        private void OnEnable()
        {
            EventHandler.GetPointEvent += OnGetPointEvent;
            EventHandler.GameOverEvent += OnGameOverEvent;
        }

        /// <summary>
        /// 在游戏结束时调用，更新并保存排行榜数据。
        /// </summary>
        private void OnGameOverEvent()
        {
            if (!scoreList.Contains(score))
            {
                scoreList.Add(score);
            }
            scoreList.Sort();
            scoreList.Reverse();
        
            try
            {
                File.WriteAllText(dataPath, JsonConvert.SerializeObject(scoreList));
            }
            catch (IOException e)
            {
                Debug.LogError($"Failed to write leaderboard data: {e.Message}");
            }
        }

        /// <summary>
        /// 在获得分数时调用，更新当前游戏得分。
        /// </summary>
        /// <param name="point">获得的分数。</param>
        private void OnGetPointEvent(int point)
        {
            score = point;
        }

        /// <summary>
        /// 从文件中读取排行榜数据。
        /// </summary>
        /// <returns>排行榜数据列表。</returns>
        private List<int> GetScoreListData()
        {
            // 检查指定路径的文件是否存在
            if (File.Exists(dataPath))
            {
                // 读取文件中的所有文本
                string json = File.ReadAllText(dataPath);
                // 将读取的JSON字符串反序列化为List<int>对象并返回
                return JsonConvert.DeserializeObject<List<int>>(json);
            }
            // 如果文件不存在，返回一个新的空List<int>对象
            return new List<int>();
            
        }

        /// <summary>
        /// 在组件禁用时调用，注销事件处理函数。
        /// </summary>
        private void OnDisable()
        {
            EventHandler.GetPointEvent -= OnGetPointEvent;
            EventHandler.GameOverEvent -= OnGameOverEvent;
        }
    }
}
