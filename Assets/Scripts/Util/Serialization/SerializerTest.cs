using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SerializerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //test_Vector2Int();

        {
/*            test_string();
            test_dict();

            test_struct_array();
            test_class_array();

            test_struct_list();
            test_struct();

            test_fixed_float(0x100000, 0.1f);
            test_fixed_float(0x100000, 110.1f);

            test_fixed_byte(0x100, 1);
            test_fixed_ushort(0x10000, 1);

            test_vary_sbyte(0x100, 1);
            test_vary_short(0x10000, 1);

            test_nested_array();*/
        }
    }

    void test_Vector2Int() {

        Debug.Log("---- test_struct ----");

        var src = new Vector2Int(2, 3);

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io(ref src);

        Debug.Log(StringUtil.binToHex(buf));

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        var dst = new Vector2Int();
        de.io(ref dst);

        Debug.Assert(src.x == dst.x);
        Debug.Assert(src.y == dst.y);

    }


    void test_nested_array()
    {
        int[][] vs = new int[20][];

        for(uint i = 0; i < 20; i++) {
            vs[i] = new int[10] {1,1,1,1,1,1,1,1,1,1 };
        }

        var buf = new List<byte>();
        {
            var se = new BinSerializer(buf);
            se.io(ref vs);
        }

        Debug.Log(buf.Count);

        int[][] vs1 = new int[0][];
        {
            var de = new BinDeserializer(buf.ToArray());
            de.io(ref vs1);
        }

        for(int i = 0; i < vs1.Length; i++)
        {
            for(int j = 0; j < vs1[i].Length; j++)
            {
                Debug.Assert(vs[i][j] == vs1[i][j]);
            }
        }
    }


	static void test_fixed_byte(ulong n, ulong step)
	{
		Debug.Log("---- test_fixed_byte ----");
		var buf = new List<byte>();

		{
			var se = new BinSerializer(buf);
			for (ulong i = 0; i < n; i += step)
			{
				byte t = (byte)i;
				se.io_fixed(ref t);
			}
		}

		//		Console.WriteLine(StringUtil.binToHex(buf));

		{
			System.Span<byte> deBuf = buf.ToArray();

			var se = new BinDeserializer(deBuf);
			for (ulong i = 0; i < n; i += step)
			{
				byte expected = (byte)i;
				byte t = 0;
				se.io_fixed(ref t);
				Debug.Assert(t == expected);
			}
		}
	}

    static void test_fixed_ushort(ulong n, ulong step)
    {
        Debug.Log("---- test_fixed_ushort ----");

        var buf = new List<byte>();

        {
            var se = new BinSerializer(buf);
            for (ulong i = 0; i < n; i += step)
            {
                ushort t = (ushort)i;
                se.io_fixed(ref t);
            }
        }

        //		Console.WriteLine(StringUtil.binToHex(buf));

        {
            System.Span<byte> debuf = buf.ToArray();
            var se = new BinDeserializer(debuf);
            for (ulong i = 0; i < n; i += step)
            {
                ushort expected = (ushort)i;
                ushort t = 0;
                se.io_fixed(ref t);
                Debug.Assert(t == expected);
            }
        }
    }

    static void test_fixed_float(ulong n, float step)
    {
        Debug.Log("---- test_fixed_float ----");

        var buf = new List<byte>();

        {
            var se = new BinSerializer(buf);
            for (ulong i = 0; i < n; i++)
            {
                float t = (float)i * step;
                se.io(ref t);
            }
        }

        //		Console.WriteLine(StringUtil.binToHex(buf));

        {
            System.Span<byte> debuf = buf.ToArray();
            var se = new BinDeserializer(debuf);
            for (ulong i = 0; i < n; i++)
            {
                float expected = (float)i * step;
                float t = 0;
                se.io(ref t);
                Debug.Assert(t == expected);
            }
        }
    }

    static void test_vary_sbyte(ulong n, ulong step)
    {
        Debug.Log("---- test_vary_sbyte ----");

        var buf = new List<byte>();
        {
            var se = new BinSerializer(buf);
            for (ulong i = 0; i < n; i += step)
            {
                sbyte t = (sbyte)i;
                se.io(ref t);
            }
        }

        //		Console.WriteLine(StringUtil.binToHex(buf));

        {
            System.Span<byte> debuf = buf.ToArray();
            var se = new BinDeserializer(debuf);
            for (ulong i = 0; i < n; i += step)
            {
                sbyte expected = (sbyte)i;
                sbyte t = 0;
                se.io(ref t);
                Debug.Assert(t == expected);
            }
        }
    }

    static void test_vary_short(ulong n, ulong step)
    {
        Debug.Log("---- test_vary_short ----");

        var buf = new List<byte>();
        {
            var se = new BinSerializer(buf);
            for (ulong i = 0; i < n; i += step)
            {
                short t = (short)i;
                se.io(ref t);
            }
        }

        //		Console.WriteLine(StringUtil.binToHex(buf));

        {
            System.Span<byte> debuf = buf.ToArray();
            var se = new BinDeserializer(debuf);
            for (ulong i = 0; i < n; i += step)
            {
                short expected = (short)i;
                short t = 0;
                se.io(ref t);
                Debug.Assert(t == expected);
            }
        }
    }

    static void test_string()
    {
        Debug.Log("---- test_string ----");
        string srcHello = "hello";
        string srcEmpty = "";
        string srcNull = null;

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io(ref srcHello);
        se.io(ref srcEmpty);
        se.io(ref srcNull);

        Debug.Log(StringUtil.binToHex(buf));

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);

        string dstHello = null;
        string dstEmpty = null;
        string dstNull = null;

        de.io(ref dstHello);
        de.io(ref dstEmpty);
        de.io(ref dstNull);

        Debug.Assert(srcHello == dstHello);
        Debug.Assert(srcEmpty == dstEmpty);
        Debug.Assert("" == dstNull);
    }

    struct MyStruct
    {
        public int i;
        public float f;
    }

    class MyClass
    {
        public int i;
        public float f;
    }

    static void test_struct()
    {
        Debug.Log("---- test_struct ----");

        var src = new MyStruct();
        src.i = 100;
        src.f = 1.2f;

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io(ref src);

        Debug.Log(StringUtil.binToHex(buf));

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        var dst = new MyStruct();
        de.io(ref dst);

        Debug.Assert(src.i == dst.i);
        Debug.Assert(src.f == dst.f);
    }

    static void test_struct_array()
    {
        Debug.Log("---- test_struct_array ----");

        var srcList = new MyStruct[2];
        for (int i = 0; i < srcList.Length; i++)
        {
            srcList[i] = new MyStruct();
            srcList[i].i = 100 * i;
            srcList[i].f = 1.2f * i;
        }

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io_object(srcList);

        Debug.Log(StringUtil.binToHex(buf));

        var dstList = new MyStruct[0];

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        de.io(ref dstList);

        for (int i = 0; i < srcList.Length; i++)
        {
            Debug.Assert(srcList[i].i == dstList[i].i);
            Debug.Assert(srcList[i].f == dstList[i].f);
        }
    }

    static void test_class_array()
    {
        Debug.Log("---- test_class_array ----");

        var srcList = new MyClass[2];
        for (int i = 0; i < srcList.Length; i++)
        {
            srcList[i] = new MyClass();
            srcList[i].i = 100 * i;
            srcList[i].f = 1.2f * i;
        }

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io_object(srcList);

        Debug.Log(StringUtil.binToHex(buf));

        var dstList = new MyClass[0];

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        de.io(ref dstList);

        for (int i = 0; i < srcList.Length; i++)
        {
            Debug.Assert(srcList[i].i == dstList[i].i);
            Debug.Assert(srcList[i].f == dstList[i].f);
        }
    }

    static void test_struct_list()
    {
        Debug.Log("---- test_struct_list ----");

        var srcList = new List<MyStruct>();

        var src = new MyStruct();
        src.i = 100;
        src.f = 1.2f;

        for (int i = 0; i < 2; i++)
        {
            srcList.Add(src);
        }

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io_object(srcList);

        Debug.Log(StringUtil.binToHex(buf));

        var dstList = new List<MyStruct>();

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        de.io(ref dstList);

        for (int i = 0; i < srcList.Count; i++)
        {
            Debug.Assert(srcList[i].i == dstList[i].i);
            Debug.Assert(srcList[i].f == dstList[i].f);
        }
    }

    static void test_dict()
    {
        Debug.Log("---- test_dict ----");

        var srcList = new Dictionary<int, int>();

        for (int i = 0; i < 20; i++)
        {
            srcList.Add(i, i * 10);
        }

        var buf = new List<byte>();
        var se = new BinSerializer(buf);
        se.io_object(srcList);

        Debug.Log(StringUtil.binToHex(buf));

        var dstList = new Dictionary<int, int>();

        System.Span<byte> debuf = buf.ToArray();
        var de = new BinDeserializer(debuf);
        de.io(ref dstList);

        for (int i = 0; i < srcList.Count; i++)
        {
            Debug.Assert(srcList[i] == dstList[i]);
            Debug.Assert(srcList[i] == dstList[i]);
        }
    }





}
