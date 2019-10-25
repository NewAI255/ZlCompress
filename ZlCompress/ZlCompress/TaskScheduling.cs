using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZlCompress
{

    /// <summary>
    /// 任务调用
    /// </summary>
    /// <typeparam name="T">任务项类型</typeparam>
    /// <typeparam name="TR">执行后返回数据项</typeparam>
    public class TaskScheduling<T, TR>
    {
        //开启线程数
        private int _threadCount;

        //任务集合
        private readonly Queue<T> _tasks;

        //执行结果
        private readonly List<TR> _returnList;

        //是否正在执行
        private bool _isExecute;

        //执行方法
        private Func<T, TR> _taskFunc;

        //出现异常后处理方法
        private Func<T, Exception, TR> _exceptionFunc;

        /// <summary>
        /// 是否正在执行
        /// </summary>
        public bool IsExecute
        {
            get { return _isExecute; }
            private set
            {
                lock (this)
                {
                    _isExecute = value;
                }
            }
        }

        /// <summary>
        /// 执行线程数
        /// </summary>
        public int ThreadCount
        {
            get { return _threadCount; }
            set
            {
                lock (this)
                {
                    if (IsExecute) throw new Exception("任务正在执行！无法设置线程数！");
                    _threadCount = value;
                }
            }
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        public Func<T, TR> TaskFunc
        {
            get { return _taskFunc; }
            set
            {
                lock (this)
                {
                    if (IsExecute) throw new Exception("任务正在执行！无法设置执行方法！");
                    _taskFunc = value;
                }
            }
        }
        /// <summary>
        /// 出现异常后处理方法
        /// </summary>
        public Func<T, Exception, TR> ExceptionFunc
        {
            get { return _exceptionFunc; }
            set
            {
                lock (this)
                {
                    if (IsExecute) throw new Exception("任务正在执行！无法设置出现异常后处理方法！");
                    _exceptionFunc = value;
                }
            }
        }

        /// <summary>
        /// 初始化任务调度
        /// </summary>
        /// <param name="tasks">初始化任务</param>
        /// <param name="threadCount">创建调度线程数</param>
        public TaskScheduling(IEnumerable<T> tasks, int threadCount = 10)
            // ReSharper disable once PossibleMultipleEnumeration
            : this(threadCount, tasks.Count())
        {
            // ReSharper disable once PossibleMultipleEnumeration
            AddRange(tasks);
        }

        /// <summary>
        /// 初始化任务调度
        /// </summary>
        /// <param name="threadCount">创建调度线程数</param>
        /// <param name="capacity">队列池大小</param>
        public TaskScheduling(int threadCount = 10, int capacity = 0)
        {
            _threadCount = Math.Max(threadCount, 1); //至少一个线程
            _tasks = new Queue<T>(capacity);
            _returnList = new List<TR>(capacity);
        }

        /// <summary>
        /// 添加执行任务
        /// </summary>
        public void Add(T item)
        {
            if (IsExecute) throw new Exception("任务正在执行！无法添加任务！");
            _tasks.Enqueue(item);
        }

        /// <summary>
        /// 批量添加执行任务
        /// </summary>
        public void AddRange(IEnumerable<T> list)
        {
            if (IsExecute) throw new Exception("任务正在执行！无法批量添加任务！");
            foreach (var l in list)
            {
                _tasks.Enqueue(l);
            }
        }

        /// <summary>
        /// 执行任务【同步进行】
        /// </summary>
        /// <returns>执行返回的结果</returns>
        public List<TR> Execute()
        {
            lock (this)
            {
                if (IsExecute) throw new Exception("任务正在执行！无法再次执行任务！");
                IsExecute = true;
            }
            _returnList.Clear(); //清除存储执行结果的集合
            var threadCount = Math.Min(_tasks.Count, _threadCount); //如果执行的任务少于执行线程数就不需要开启全部线程
            var tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Factory.StartNew(ExecuteTask); //开始执行任务
            }
            Task.WaitAll(tasks); //等待线程执行完毕
            IsExecute = false;//执行完成
            return _returnList;
        }

        /// <summary>
        /// 获取一个任务
        /// </summary>
        /// <param name="task">任务对象</param>
        /// <returns>是否获取成功</returns>
        private bool GetTask(out T task)
        {
            lock (this)
            {
                if (_tasks.Count < 1)
                {
                    task = default(T);
                    return false;
                }

                task = _tasks.Dequeue();
                return true;
            }
        }

        /// <summary>
        /// 保存执行结果
        /// </summary>
        private void SaveRe(TR re)
        {
            lock (this)
            {
                _returnList.Add(re);
            }
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        private void ExecuteTask()
        {
            T task;
            while (GetTask(out task)) //获取一个任务
            {
                var re = default(TR);
                try
                {
                    re = TaskFunc(task);
                }
                catch (Exception ex)
                {
                    if (ExceptionFunc != null)
                    {
                        re = ExceptionFunc(task, ex); //执行异常处理方法
                    }
                }
                finally
                {
                    SaveRe(re);
                }
            }
        }
    }
}
