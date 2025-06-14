public enum Shape { Circle, Square, Triangle }
public enum FrameColor { Red, Blue, Green }
public enum Fruit { Cat, Dog, Fox }

[System.Serializable]
public class FigureData
{
    public Shape shape;
    public FrameColor frameColor;
    public Fruit fruit;

    public override bool Equals(object obj)
    {
        if (obj is FigureData other)
            return shape == other.shape && frameColor == other.frameColor && fruit == other.fruit;
        return false;
    }

    public override int GetHashCode()
    {
        return shape.GetHashCode() ^ frameColor.GetHashCode() ^ fruit.GetHashCode();
    }
}