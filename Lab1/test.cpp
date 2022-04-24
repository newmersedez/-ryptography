#include
#include <string.h>

using namespace std;

unsigned int ip[2][64]={{40,8,48,16,56,24,64,32,39,7,47,15,55,23,63,31,38,6,46,14,54,22,62,30,37,5,45,13,53,21,61,29,
36,4,44,12,52,20,60,28,35,3,43,11,51,19,59,27,34,2,42,10,50,18,58,26,33,1,41,9,49,17,57,25},
{58,50,42,34,26,18,10,2,60,52,44,36,28,20,12,4,62,54,46,38,30,22,14,6,64,56,48,40,32,24,16,8,
57,49,41,33,25,17,9,1,59,51,43,35,27,19,11,3,61,53,45,37,29,21,13,5,63,55,47,39,31,23,15,7}};
unsigned int g[56]={57,49,41,33,25,17,9,1,58,50,42,34,26,18,10,2,59,51,43,35,27,19,11,3,60,52,44,36,63,55,47,39,31,23,15,
7,62,54,46,38,30,22,14,6,61,53,45,37,29,21,13,5,28,20,12,4};
unsigned int shift[16]={1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};
unsigned int h[48]={14,17,11,24,1,5,3,28,15,6,21,10,23,19,12,4,26,8,16,7,27,20,13,2,41,52,31,37,47,55,30,40,51,45,33,48,
44,49,39,56,34,53,46,42,50,36,29,32};
unsigned int E[48]={32,1,2,3,4,5,4,5,6,7,8,9,8,9,10,11,12,13,12,13,14,15,16,17,16,17,18,19,20,21,20,21,22,23,24,25,24,
25,26,27,28,29,29,29,30,31,32,1};
unsigned int p[32]={16,7,20,21,29,12,28,17,1,15,23,26,5,18,31,10,2,8,24,14,22,27,3,9,19,13,30,6,22,11,4,25};
//bool keys[16][48];
unsigned int s[8][4][16]=
{
{
{14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
{0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
{4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
{15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
},
{
{15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
{3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
{0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
{13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
},
{
{10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
{13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
{13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
{1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}
},
{
{7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
{13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
{10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
{3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
},
{
{2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},
{14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
{4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
{11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}
},
{
{12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
{10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
{9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
{4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}
},
{
{4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
{13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
{1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
{6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}
},
{
{13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
{1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
{7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
{2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}
}
};

unsigned long long int buf = 0; // зачем-то завели буфер
unsigned int Bmas[8], Bmasnew[8]; //буферный массив, зачем
unsigned long long int mask = 1; //я разобрался что такое "битовая маска", не понятна строка, почему приравняли к 1
unsigned long long int IP = 0;
unsigned long long int L, R, B, Bs;
unsigned long long int key, PP;
unsigned long long int Rlong;

unsigned long long int _IP(unsigned long long int T, bool flag){ //объясните пожалуйста смысл цикла, берётся элемент массива, на которого накладывается маска и помещается в буфер?
for(int i=0; i<64; i++){
buf = buf|(T&(mask<<ip[flag][i]));
mask = 1; //зачем
}
return buf;
}

unsigned long long int Key(unsigned long long int K){
unsigned long long int C = 0; //28 bits
unsigned long long int D = 0; //28 bits
mask = 1;

for(int i=0; i<28; i++){
mask = 1;
mask<<=g[i];
buf = mask&K;
buf>>=g[i];
buf<<=i;
C = C|buf;
}
for(int i=28; i<56; i++){
mask = 1;
mask<<=g[i];
buf = mask&K;
buf>>=g[i];
buf<<=i;
D = D|buf;
}


//циклический регистр?
unsigned long long int bufC = 0;
unsigned long long int bufD = 0;
unsigned long long int maskC = 1;
unsigned long long int maskD = 1<<28;
for(int i=0; i<16; i++){
for(int j=0; j<shift[i]; j++){
bufC = bufC|(maskC<<=j)&C;
bufD = bufD|(maskD<<=j)&D;
maskC = 1;
maskD = 1<<28;
}
C<<=shift[i];
D<<=shift[i];
maskC = 1;
maskD = 1<<28;
for(int j=0; j<shift[i]; j++){
C = C|(maskC<<=j)&bufC;
D = D|(maskD<<=j)&bufD;
maskC = 1;
maskD = 1<<28;
}

};
unsigned long long int CD = C|D; //56 bits
unsigned long long int Knew = 0; //48 bits
buf = 0;
mask = 1;
for(int i=0; i<48; i++){
mask<<=h[i];
buf = mask&CD;
buf>>=h[i];
buf<<=i;
Knew = Knew|buf;
}
return Knew;
}

unsigned long long int Sbox(unsigned long long int B){
// S-box
mask=63;
buf = 0;
unsigned long long Bss = 0;//
for(int i=0; i<8; i++){
buf = ((mask<<i)&B)>>i;
unsigned long long int b1b6 = ((buf&(1<<5))>>4)|(buf&1);
unsigned long long int b2b3b4b5 = (buf>>1)&15;
Bss = Bs|(s[i][b1b6][b2b3b4b5]<<(i*4));// 0..011111..11
}

return Bss;
}

unsigned long long int F(unsigned long long int R1, unsigned long long int K){

buf = 0;
Rlong = 0;
for(int i=0; i<48; i++){
mask = 1;
mask<<=E[i];
buf = mask^R1;
buf>>=E[i];
buf<<=i;
Rlong = Rlong|buf;
}
Rlong = Rlong^K; // В яке іде на S-box
Bs = Sbox(Rlong);

PP = 0;// результат Р перестановки
buf = 0;
mask = 1;
for(int i=0; i<32; i++){
mask<<=p[i];
buf = mask&Bs;
buf>>=p[i];
buf<<=i;
PP = PP|buf;
}

return PP;
}

unsigned long long int DES(unsigned long long int T){
IP = _IP(T, 1);
// left + right
L = IP;
printf("\n%i\n", L);
//L = IP<<32;
R = IP>>32;
printf("\n%i\n", R);
//------------
for(int round=0; round<16; round++){
buf = L;
L = R;
R = buf^F(R,Key(233465)); // K = ? дописали генерацию ключа
// S-box

unsigned long long int Res = _IP((R<<32)|L, 0);
return Res;
}

}

int main()
{
unsigned long long int a = 3333333333; //почему
unsigned long long int res = DES(a);
printf("%i", res);
system("PAUSE");
return 0;
}