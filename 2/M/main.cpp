#include <iostream>

int main() {
    for (int i = 0; i <= 10; i++) {
        std::cout << "a" << i << " 0 _ -> a" << (i + 1) << " 0 > _ ^" << std::endl;
        std::cout << "a" << i << " 1 _ -> a" << (i + 1) << " 1 > _ ^" << std::endl;
        std::cout << "a" << i << " | _ -> a" << i << "b0 | > _ ^" << std::endl;
        for (int j = 0; j <= 10; j++) {
            std::cout << "a" << i << "b" << j << " 0 _ -> a" << i << "b" << j + 1 << " 0 > _ ^" << std::endl;
            std::cout << "a" << i << "b" << j << " 1 _ -> a" << i << "b" << j + 1 << " 1 > _ ^" << std::endl;
            if (i < j) {
                std::cout << "a" << i << "b" << j << " | _ -> prev | < _ ^" << std::endl;
                std::cout << "a" << i << "b" << j << " _ _ -> check _ < _ <" << std::endl;
            } else if (i == j) {
                std::cout << "a" << i << "b" << i << " | _ -> cmp | < _ ^" << std::endl;
                std::cout << "a" << i << "b" << i << " _ _ -> cmplast _ < _ ^" << std::endl;
            } else {
                std::cout << "a" << i << "b" << j << " | _ -> swap | < _ ^" << std::endl;
                std::cout << "a" << i << "b" << j << " _ _ -> swaplast _ < _ ^" << std::endl;
            }
        }
    }
    
    return 0;
}
