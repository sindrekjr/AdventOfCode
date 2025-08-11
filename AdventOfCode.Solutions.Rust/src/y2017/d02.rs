use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day02::solve_part_one(input),
        Part::P2 => Day02::solve_part_two(input),
    }
}

struct Day02;

#[allow(unused_variables)]
impl Solution for Day02 {
    fn solve_part_one(input: String) -> String {
        input
            .lines()
            .map(|line| {
                let (min, max) = line
                    .split_whitespace()
                    .fold((i32::MAX, i32::MIN), |(min, max), n| {
                        (min.min(n.parse().unwrap()), max.max(n.parse().unwrap()))
                    });

                max - min
            })
            .sum::<i32>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        input
            .lines()
            .map(|line| {
                let nums = line
                    .split_whitespace()
                    .map(|n| n.parse().unwrap())
                    .collect::<Vec<i32>>();

                nums.iter()
                    .find_map(|n| nums.iter().find(|m| n != *m && n % *m == 0).map(|m| n / m))
                    .unwrap()
            })
            .sum::<i32>()
            .to_string()
    }
}
