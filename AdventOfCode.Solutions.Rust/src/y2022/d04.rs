use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day04::solve_part_one(input),
        Part::P2 => Day04::solve_part_two(input),
    }
}

struct Day04;
impl Solution for Day04 {
    fn solve_part_one(input: String) -> String {
        String::new()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}
