use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day13::solve_part_one(input),
        Part::P2 => Day13::solve_part_two(input),
    }
}

struct Day13;
impl Solution for Day13 {
    fn solve_part_one(input: String) -> String {
        format!("Input is {}", input)
    }

    fn solve_part_two(input: String) -> String {
        format!("Input is {}", input)
    }
}