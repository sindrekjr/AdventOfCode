use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day05::solve_part_one(input),
        Part::P2 => Day05::solve_part_two(input),
    }
}

struct Day05;
impl Solution for Day05 {
    fn solve_part_one(input: String) -> Option<String> {
        String::new()
    }

    fn solve_part_two(input: String) -> Option<String> {
        String::new()
    }
}
