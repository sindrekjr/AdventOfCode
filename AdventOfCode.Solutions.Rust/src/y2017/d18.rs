use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day18::solve_part_one(input),
        Part::P2 => Day18::solve_part_two(input),
    }
}

struct Day18;

#[allow(unused_variables)]
impl Solution for Day18 {
    fn solve_part_one(input: String) -> String {
        String::from("Unsolved")
    }

    fn solve_part_two(input: String) -> String {
        String::from("Unsolved")
    }
}
