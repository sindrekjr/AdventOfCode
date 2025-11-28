use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day01::solve_part_one(input),
        Part::P2 => Day01::solve_part_two(input),
    }
}

struct Day01;

#[allow(unused_variables)]
impl Solution for Day01 {
    fn solve_part_one(input: String) -> Option<String> {
        None
    }

    fn solve_part_two(input: String) -> Option<String> {
        None
    }
}
