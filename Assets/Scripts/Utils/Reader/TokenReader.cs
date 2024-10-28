using System;
using System.Collections.Generic;

public class TokenReader
{
    public static readonly char[] Separator = { ',', '\t' };
    
    private int _mPos = 0;
    private bool _mValid;
    private string[] _mTokens;

    public int ReadPos => _mPos;

    public delegate object ParseFunc(string str);

    private static readonly Dictionary<System.Type, ParseFunc> _ParseFuncMap = new()
    {
        { typeof(bool), str => int.Parse(str) != 0 },
        { typeof(char), str => char.Parse(str) },
        { typeof(byte), str => byte.Parse(str) },
        { typeof(Int16), str => Int16.Parse(str) },
        { typeof(Int32), str => Int32.Parse(str) },
        { typeof(Int64), str => Int64.Parse(str) },
        { typeof(UInt16), str => Int16.Parse(str) },
        { typeof(UInt32), str => Int32.Parse(str) },
        { typeof(UInt64), str => Int64.Parse(str) },
        { typeof(float), str => float.Parse(str) },
        { typeof(double), str => double.Parse(str) },
        { typeof(string), str => str },
    };
    
    private static object Parse(string str, System.Type type)
    {
        return _ParseFuncMap.TryGetValue(type, out var parseFunc) ? parseFunc(str) : null;
    }

    public TokenReader()
    {
        _mPos = 0;
        _mValid = true;
    }

    public TokenReader(string line)
    {
        GenerateTokens(line);
    }
    
    public TokenReader(string line, char[] separator)
    {
        GenerateTokens(line, separator);
    }
    
    public void GenerateTokens(string line, char[] separator = null)
    {
        _mTokens = line.Split(separator ?? Separator, StringSplitOptions.RemoveEmptyEntries);

        _mPos = 0;
        _mValid = true;
    }

    public void Advance(int offset = 1)
    {
        _mPos += offset;
    }

    public void Reset()
    {
        _mPos = 0;
        _mValid = true;
    }
    
    public bool IsReadValid()
    {
        return _mValid;
    }
    
    public void MarkReadInvalid()
    {
        _mValid = false;
    }
    
    public bool Read<T>(out T value)
    {
        value = default;

        if (!IsReadValid())
            return false;

        if (_mTokens.Length <= _mPos)
        {
            MarkReadInvalid();
            return false;
        }

        var str = _mTokens[_mPos++];
        var result = Parse(str, typeof(T));
        if (result == null)
        {
            MarkReadInvalid();
            return false;
        }

        value = (T)result;
        return true;
    }

    public T Read<T>()
    {
        if (!IsReadValid())
            return default;

        if (_mTokens.Length <= _mPos)
        {
            MarkReadInvalid();
            return default;
        }
        
        return (T)Parse(_mTokens[_mPos++], typeof(T));
    }
    
    public List<T> ReadVec<T>()
    {
        if (!IsReadValid())
            return default;

        if (_mTokens.Length <= _mPos)
        {
            MarkReadInvalid();
            return default;
        }

        var reader = new TokenReader(_mTokens[_mPos++], new []{'|'});
        var vec = new List<T>();

        for (int i = 0; i < reader._mTokens.Length; i++)
        {
            var val = reader.Read<T>();
            if (!reader.IsReadValid())
                break;
            
            vec.Add(val);
        }

        return vec;
    }
}