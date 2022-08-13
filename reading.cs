using System.Text;

public class reading
{
    char phase;
    int potential;
    float amperage;

    public reading(char phase, int potential, float amperage)
    {
        this.phase = phase;
        this.potential = potential;
        this.amperage = amperage;
    }

    public char getPhase()
    {
        return this.phase;
    }

    public int getPotential()
    {
        return this.potential;
    }

    public float getAmperage()
    {
        return this.amperage;
    }

    public string toString()
    {
		var str = new StringBuilder();
		str.Append("Phase: [");
		str.Append(phase);
		str.Append("]\tPotential: [");
		str.Append(potential.ToString());
		str.Append("]\tAmperage: [");
		str.Append(amperage.ToString());
		str.Append("]");
		return str.ToString();
    }
}
