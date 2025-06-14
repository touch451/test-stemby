using System;
using Types;

namespace Scripts
{
    [Serializable]
    public class FigureData
    {
        public FormType form;
        public FrameType frame;
        public FruitType fruit;

        public override bool Equals(object obj)
        {
            if (obj is FigureData other)
                return form == other.form && frame == other.frame && fruit == other.fruit;

            return false;
        }

        public override int GetHashCode()
        {
            return form.GetHashCode() ^ frame.GetHashCode() ^ fruit.GetHashCode();
        }
    }
}