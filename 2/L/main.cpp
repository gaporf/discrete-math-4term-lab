#include <iostream>
#include <vector>

char get_hex(size_t i) {
    if (i < 10) {
        return std::to_string(i)[0];
    } else {
        return 'a' + (i - 10);
    }
}

int main() {
    for (size_t i = 0; i <= 15; i++) {
        std::cout << "right1 " << get_hex(i) << " -> right1 " << get_hex(i) << " >" << std::endl;
    }
    return 0;
}
