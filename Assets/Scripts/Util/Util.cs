using System.Collections.Generic;


public static class Util
{
    public static int EnumCount<T>() {
        return System.Enum.GetValues(typeof(T)).Length;
    }

    public static void UnsortedRemoveAt<T>(this List<T> list, int index) {
        var lastIndex = list.Count > 1 ? list.Count - 1 : 0;
        if (lastIndex != 0) {
            list[index] = list[lastIndex];
        }
        list.RemoveAt(lastIndex);
    }




}
