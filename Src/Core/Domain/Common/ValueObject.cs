namespace Domain.Common;

public abstract class ValueObject
{
    //Override the equal operator 
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        //XOR(^) left and righ =>if one of it be null(true)then return false
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right) != false;
    }


    //Over not equal operator 
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }



    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        if (obj==null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        //Get the enumerable of atomic values in the value object
        var thisValue = GetAtomicValues().GetEnumerator();
        var otherValue = other.GetAtomicValues().GetEnumerator();

        //check the quality of per enumerator of object
        while (thisValue.MoveNext() && otherValue.MoveNext())
        {
            if (thisValue.Current is null ^ otherValue.Current is null)
            {
                return false;       
            }

            //Check equality of every current value of enumerator 
            if (thisValue.Current!=null &&
                !thisValue.Current.Equals(otherValue.Current)
                )
            {
                return false;
            }
        }
        return !thisValue.MoveNext() && !otherValue.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}