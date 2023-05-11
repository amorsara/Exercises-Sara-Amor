#include <iostream>
#include <cstdlib>
#include <stdio.h>
using namespace std;

void menu();

void rectangle(int width, int height) {
    if (abs(height - width) > 5) {
        cout << "Area : " << width * height << endl;
    }
    else {
        cout << "perimeter : " << (width + height) * 2 << endl;
    }
    menu();
}

void triangularPerimeter(int width, int height) {
    int r = height * height + (width / 2) * (width / 2);
    r = sqrt(r);
    cout << "perimeter " << r * 2 + width << endl;
}

void printTriangular(int width, int height) {
    if (width % 2 == 0 || width > height * 2) {
        cout << "can not print this triangular" << endl;
    }
    else {
        int cntNums = (width - 2) / 2;
        int numRow = (height - 2) / cntNums;
        int secRow = (height - 2) % cntNums + numRow;
        int k;
        for (int i = 1; i <= width; i += 2)
        {
            if (i == 1 || i == width) {
                k = 1;
            }
            else {
                if (i == 3) {
                    k = secRow;
                }
                else {
                    k = numRow;
                }
            }
            for (int j = 0; j < k; j++)
            {
                for (int n = 0; n < (width - i) / 2; n++)
                {
                    cout << " ";
                }
                for (int n = 0; n < i; n++)
                {
                    cout << "*";
                }
                cout << " " << endl;
            }
        }
    }
}

void triangular(int width, int height) {
    int optionTriangular;
    cout << "choose 1 - to perimeter, 2 - print triangular" << endl;
    cin >> optionTriangular;
    switch (optionTriangular) {
    case 1:
        triangularPerimeter(width, height);
        break;
    case 2:
        printTriangular(width, height);
        break;
    }
    menu();
}


void menu() {
    int option, width, height;
    cout << "choose 1 - rectangle tower,  2 - triangular tower, 3 - exit" << endl;
    cin >> option;
    if (option != 3) {
        cout << "enter height" << endl;
        cin >> height;
        cout << "enter width" << endl;
        cin >> width;
    }
    switch (option) {
    case 1:
        rectangle(width, height);
        break;
    case 2:
        triangular(width, height);
        break;
    case 3:
        exit(0);
    }
}

int main()
{
    menu();

}

