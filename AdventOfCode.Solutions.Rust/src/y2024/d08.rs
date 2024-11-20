use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day08::solve_part_one(input),
        Part::P2 => Day08::solve_part_two(input),
    }
}

struct Day08;
impl Solution for Day08 {
    fn solve_part_one(input: String) -> String {
        String::new()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}
