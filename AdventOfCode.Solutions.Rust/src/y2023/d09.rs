use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day09::solve_part_one(input),
        Part::P2 => Day09::solve_part_two(input),
    }
}

struct Day09;
impl Solution for Day09 {
    fn solve_part_one(input: String) -> Option<String> {
        String::new()
    }

    fn solve_part_two(input: String) -> Option<String> {
        String::new()
    }
}
