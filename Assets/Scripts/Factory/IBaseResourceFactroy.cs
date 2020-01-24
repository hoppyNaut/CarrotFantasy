/****************************************************
	文件：IBaseResourceFactroy.cs
	作者：Shen
	邮箱:  879085103@qq.com
	日期：2019/11/17 10:54   	
	功能：资源工厂接口(泛型为指定的资源类型)
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseResourceFactory<T>
{
    T GetSingleResource(string resPath);
}
