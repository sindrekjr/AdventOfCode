use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day02::solve_part_one(input),
        Part::P2 => Day02::solve_part_two(input),
    }
}

struct Day02;
impl Solution for Day02 {
    fn solve_part_one(input: String) -> String {
        String::new()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}
