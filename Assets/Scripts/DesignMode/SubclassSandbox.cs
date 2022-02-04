namespace DesignMode.SubclassSandbox

{
    public abstract class BaseSandbox
    {
        /// <summary>
        /// 由子类实现的抽象方法
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// 提供给子类的方法
        /// </summary>
        protected void Fuc1() { }
        protected void Fuc2() { }
        protected void Fuc3() { }
        protected void Fuc4() { }
    }
}