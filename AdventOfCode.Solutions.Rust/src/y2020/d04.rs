use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day04::solve_part_one(input),
        Part::P2 => Day04::solve_part_two(input),
    }
}

struct Day04;

#[allow(unused_variables)]
impl Solution for Day04 {
    fn solve_part_one(input: String) -> Option<String> {
        None
    }

    fn solve_part_two(input: String) -> Option<String> {
        None
    }
}
