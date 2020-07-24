#include "Sample.hpp"
#include <iostream>
using namespace std;
int main(int argNum, const char* arguments)
{
	int a, b, c;
	a = 1;
	b = 2;
	c = 3;
	//auto ret = AddPtr(&a, b);
	//auto ret = Add(a, b);

	int ar[5] = { 0,1,2,3,4 };
	
	auto ret = AddArray(ar, 5,5);

	cout << "配列サイズ:" << GetArraySize(ar) << endl;
	for (int i = 0; i < GetArraySize(ar); ++i)
	{
		cout << ret[i] << endl;
	}

	//cout << *ret << "," << a << endl;



	getchar();
	return 0;
}