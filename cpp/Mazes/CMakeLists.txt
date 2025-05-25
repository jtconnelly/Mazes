project(Mazes)

add_library(Mazes SHARED  graph.h digraph.h searching.h searching.cpp)

set_target_properties(Mazes PROPERTIES LINKER_LANGUAGE CXX)