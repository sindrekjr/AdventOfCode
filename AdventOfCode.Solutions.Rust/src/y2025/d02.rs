use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day02::solve_part_one(input),
        Part::P2 => Day02::solve_part_two(input),
    }
}

struct Day02;

#[allow(unused_variables)]
impl Solution for Day02 {
    fn solve_part_one(input: String) -> Option<String> {
        None
    }

    fn solve_part_two(input: String) -> Option<String> {
        None
    }
}
