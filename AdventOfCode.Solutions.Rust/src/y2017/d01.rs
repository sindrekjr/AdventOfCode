use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day01::solve_part_one(input),
        Part::P2 => Day01::solve_part_two(input),
    }
}

struct Day01;

#[allow(unused_variables)]
impl Solution for Day01 {
    fn solve_part_one(input: String) -> String {
        let ln = input.len();

        let digits = input.chars().collect::<Vec<char>>();
        digits
            .iter()
            .enumerate()
            .fold(0, |acc, (i, c)| {
                if (i + 1) == ln && c == &digits[0] {
                    acc + c.to_digit(10).unwrap() as i32
                } else if c == &digits[i + 1] {
                    acc + c.to_digit(10).unwrap() as i32
                } else {
                    acc
                }
            })
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let digits = input.chars().collect::<Vec<char>>();
        let ln = digits.len();
        let half = ln / 2;

        digits
            .iter()
            .enumerate()
            .fold(0, |acc, (i, c)| {
                let cmp = if i + half >= ln { i + half - ln } else { i + half };

                let acc = if c == &digits[cmp] {
                    acc + c.to_digit(10).unwrap() as i32
                } else {
                    acc
                };

                acc
            })
            .to_string()
    }
}
