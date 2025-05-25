find_package(GTest REQUIRED)
#add_subdirectory(googletest)
enable_testing()
#include(GoogleTest)

project(Mazes_gtest)

add_executable(Mazes_gtest main.cpp)
set_target_properties(Mazes_gtest PROPERTIES LINKER_LANGUAGE CXX)

target_link_libraries(Mazes_gtest PRIVATE GTest::gtest_main)
target_link_libraries(Mazes_gtest PUBLIC Mazes)

target_include_directories(Mazes_gtest PUBLIC ../Mazes)

gtest_discover_tests(Mazes_gtest)
