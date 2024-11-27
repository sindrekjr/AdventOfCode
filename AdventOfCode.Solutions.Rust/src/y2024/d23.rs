use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day23::solve_part_one(input),
        Part::P2 => Day23::solve_part_two(input),
    }
}

struct Day23;
impl Solution for Day23 {
    fn solve_part_one(input: String) -> String {
        format!("Input is {}", input)
    }

    fn solve_part_two(input: String) -> String {
        format!("Input is {}", input)
    }
}
