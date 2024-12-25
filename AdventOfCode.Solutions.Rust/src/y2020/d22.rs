use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day22::solve_part_one(input),
        Part::P2 => Day22::solve_part_two(input),
    }
}

struct Day22;

#[allow(unused_variables)]
impl Solution for Day22 {
    fn solve_part_one(input: String) -> String {
        String::default()
    }

    fn solve_part_two(input: String) -> String {
        String::default()
    }
}
