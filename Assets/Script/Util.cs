using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util  {
    public static List<int> GetRandomNumberInList(int num)
    {
       
        List<int> listTest = new List<int>(num);
        List<int> rs = new List<int>(num);
        for (int i=0;i<num;i++)
        {         
            listTest.Add(i);
        }
        int j = 0;
        while(listTest.Count>0)
        {
            int ran = Random.Range(0,listTest.Count);
            rs.Add(listTest[ran]);
            listTest.RemoveAt(ran);
            j++;          
        }
        return rs;
    }

	
}
