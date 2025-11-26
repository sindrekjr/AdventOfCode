use std::collections::HashMap;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day11::solve_part_one(input),
        Part::P2 => Day11::solve_part_two(input),
    }
}

struct Day11;
impl Solution for Day11 {
    fn solve_part_one(input: String) -> Option<String> {
        let blinks = 25;
        let mut memo: HashMap<(u64, u8), u64> = HashMap::new();
        Some(
            input
                .split_whitespace()
                .map(|str| stonecount(str.parse().unwrap(), blinks, &mut memo))
                .sum::<u64>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let blinks = 75;
        let mut memo: HashMap<(u64, u8), u64> = HashMap::new();
        Some(
            input
                .split_whitespace()
                .map(|str| stonecount(str.parse().unwrap(), blinks, &mut memo))
                .sum::<u64>()
                .to_string(),
        )
    }
}

fn stonecount(stone: u64, blinks: u8, memo: &mut HashMap<(u64, u8), u64>) -> u64 {
    if blinks == 0 {
        return 1;
    }

    if let Some(count) = memo.get(&(stone, blinks)) {
        return *count;
    }

    let count = match stone {
        0 => stonecount(1, blinks - 1, memo),
        num => {
            let num_str = num.to_string();
            if num_str.len() % 2 == 0 {
                let (left, right) = num_str.split_at(num_str.len() / 2);

                stonecount(left.parse().unwrap_or(0), blinks - 1, memo)
                    + stonecount(right.parse().unwrap_or(0), blinks - 1, memo)
            } else {
                stonecount(num * 2024, blinks - 1, memo)
            }
        }
    };

    memo.insert((stone, blinks), count);
    count
}
