namespace Actors
{
    /// <summary>
    /// ギミックにインタラクトするアクターのインターフェイス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    internal interface IVisitor
    {
        /// <summary>
        /// ギアハンドルにインタラクトしたときに呼び返される
        /// </summary>
        /// <param name="gearHandle">インタラクトしたギアハンドルの参照</param>
        public void Visit(GearHandle gearHandle);
        /// <summary>
        /// レバーにインタラクトしたときに呼び返される
        /// </summary>
        /// <param name="lever">インタラクトしたレバー</param>
        public void Visit(Lever lever);
        /// <summary>
        /// ゴールにインタラクトたときに呼ばれる
        /// </summary>
        /// <param name="goal">インタラクトしたゴール</param>
        public void Visit(Goal goal);
	} // IVisitor
} // Actors
