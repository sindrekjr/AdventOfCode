use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day05::solve_part_one(input),
        Part::P2 => Day05::solve_part_two(input),
    }
}

struct Day05;

#[allow(unused_variables)]
impl Solution for Day05 {
    fn solve_part_one(input: String) -> String {
        String::default()
    }

    fn solve_part_two(input: String) -> String {
        String::default()
    }
}
