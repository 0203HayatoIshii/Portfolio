using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FSystem.Texts
{
    public interface ICampus
    {
        public void Add(string text);
        public void Remove();
        public void Clear();
    }

    /// <summary>
    /// テキストの描画を行う
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public interface IWriter
    {
        public event Action<int> OnWrite;
        public event Action OnWriteToEnd;

        public void SetTextData(TextData text);
        public void UpdateWriting();
        public void WriteToEnd();
        public void ToNextData();
    }

    public struct TextData
    {
        public EntryCommand entryPoint;
    }

    public interface ICommand
    {

    }

    public class  EntryCommand : ICommand
    {

    }
    public class ExitCommand : ICommand
    {

    }

    public class OutputCommand : ICommand
    {

    }
    public class IfCommand : ICommand
    {

    }
    public class SwitchCommand : ICommand
    {

    }
    public class EventCommand : ICommand
    {

    }

}