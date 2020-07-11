#include <iostream>

int main() {
    for (int i = 0; i < 100; i++) {
        std::cout << "z" << i << " ( -> z" << (i + 1) << " _ >" << std::endl;
    }
    for (int i = 100; i > 0; i--) {
        std::cout << "z" << i << " ) -> z" << (i - 1) << " _ >" << std::endl;
        std::cout << "z" << i << " _ -> rj _ ^" << std::endl;
    }
    std::cout << "z0 ) -> rj _ ^" << std::endl;
    std::cout << "z0 _ -> ac _ ^" << std::endl;
    
    return 0;
}
