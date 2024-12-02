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
        input
            .lines()
            .filter(|l| {
                let levels: Vec<i32> = l
                    .split_whitespace()
                    .map(|level| level.parse::<i32>().unwrap())
                    .collect();

                safe(levels)
            })
            .count()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        input
            .lines()
            .filter(|l| {
                let levels: Vec<i32> = l
                    .split_whitespace()
                    .map(|level| level.parse::<i32>().unwrap())
                    .collect();

                (0..levels.len()).any(|i| {
                    let new_levels: Vec<i32> = levels[..i]
                        .iter()
                        .chain(&levels[i + 1..])
                        .cloned()
                        .collect();
                    safe(new_levels)
                })
            })
            .count()
            .to_string()
    }
}

fn safe(levels: Vec<i32>) -> bool {
    let increasing = levels[0] < levels[1];
    let mut previous: Option<i32> = None;

    for level in levels {
        if let Some(p) = previous {
            if p == level || (p - level).abs() > 3 {
                return false;
            }

            if increasing && level < p || !increasing && level > p {
                return false;
            }
        }

        previous = Some(level);
    }

    true
}
