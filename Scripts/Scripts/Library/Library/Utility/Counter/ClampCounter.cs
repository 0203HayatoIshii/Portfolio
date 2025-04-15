using System;


namespace FSystem
{
    /// <summary>
    /// �l���ŏ��l�ƍő�l�͈̔͂ŌŒ肳���J�E���^�A�ő�l���z���Ă��ő�l�܂ł̒l�Ŏ~�܂�
    /// </summary>
    /// <remarks>����� : �Έ䔹�l</remarks>
    public class ClampCounter : Counter
    {
        private int _additionValue;

        /// <summary> �J�E���g�̒ǉ��� </summary>
        public int AdditionValue { get => _additionValue; set => _additionValue = Math.Abs(value); }


        /// <summary>
        /// �l���ŏ��l�ƍő�l�͈̔͂ŌŒ肳���J�E���^�A�ő�l���z���Ă��ő�l�܂ł̒l�Ŏ~�܂�
        /// </summary>
        /// <remarks>����� : �Έ䔹�l</remarks>
        /// <param name="min">�ŏ��l</param>
        /// <param name="max">�ő�l</param>
        /// <param name="additionValue">�J�E���g�̒ǉ���</param>
        public ClampCounter(int min, int max, int additionValue = 1) : base(min, max)
        {
            AdditionValue = additionValue;
        }

        /// <summary>
        /// ����̒l���v�Z����
        /// </summary>
        /// <returns>�v�Z��̒l</returns>
        public override int ToNext()
        {
            Position += AdditionValue;
            if (Position > Max)
            {
                Position = Max;
            }
            return Position;
        }

        /// <summary>
        /// ��O�̒l���v�Z����
        /// </summary>
        /// <returns>�v�Z��̒l</returns>
        public override int ToPrev()
        {
            Position -= AdditionValue;
            if (Position < Min)
            {
                Position = Min;
            }
            return Position;
        }
    }
}