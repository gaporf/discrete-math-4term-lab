#include <iostream>

int main() {
    for (int i = 1; i <= 50; i++) {
        std::cout << "c" << i << " 0 -> z" << (i - 1) << " _ <" << std::endl;
        std::cout << "c" << i << " 1 -> o" << (i - 1) << " _ <" << std::endl;
        std::cout << "c" << i << " _ -> ac _ ^" << std::endl;
    }
    return 0;
}
