namespace DesignMode
{
    
    /// <summary>
    /// 双缓存模式
    /// </summary>
    /// <typeparam name="T">缓存数据类型</typeparam>
    public class DoubleBuffered<T> where T:class//限制为引用类型
    {
        
        public T Data{
            set =>Draw(value);
            get => _buffer[_currentBuffer];
        }
        private T[] _buffer = new T[2]; 
        private int _currentBuffer;
        
        
        /// <summary>
        /// 向缓存区写入数据
        /// </summary>
        private void Draw(T data)
        {
            _buffer[1 - _currentBuffer] = data;
        }
        
        /// <summary>
        /// 交换缓存区
        /// </summary>
        private void Swap()
        {
            _currentBuffer = 1 - _currentBuffer;
        }
    }
}