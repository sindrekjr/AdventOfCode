use std::collections::{HashMap, HashSet};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day22::solve_part_one(input),
        Part::P2 => Day22::solve_part_two(input),
    }
}

struct Day22;
impl Solution for Day22 {
    fn solve_part_one(input: String) -> Option<String> {
        Some(
            input
                .lines()
                .map(|secret| {
                    (0..2000).fold(secret.parse().unwrap(), |secret, _| next_secret(secret))
                })
                .sum::<u64>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let mut bananas: HashMap<[i8; 4], u32> = HashMap::new();

        input.lines().for_each(|secret| {
            let secrets = nth_secret(secret.parse().unwrap(), 2000);

            let mut diffs_memo: HashSet<[i8; 4]> = HashSet::new();
            for window in secrets.windows(4) {
                let diffs = [window[0].2, window[1].2, window[2].2, window[3].2];
                if diffs_memo.contains(&diffs) {
                    continue;
                } else {
                    diffs_memo.insert(diffs);
                    *bananas.entry(diffs).or_insert(0) += window[3].1 as u32;
                }
            }
        });

        Some(bananas.values().max().unwrap().to_string())
    }
}

fn next_secret(secret: u64) -> u64 {
    let multiplied = ((secret << 6) ^ secret) % 16777216;
    let divided = ((multiplied >> 5) ^ multiplied) % 16777216;
    ((divided << 11) ^ divided) % 16777216
}

fn nth_secret(secret: u64, n: u16) -> Vec<(u64, i8, i8)> {
    if n == 0 {
        return vec![];
    } else {
        let next = next_secret(secret);
        let value = (next % 10) as i8;
        let diff = value - (secret % 10) as i8;

        let mut secret_chain = vec![(next, value, diff)];
        secret_chain.extend(nth_secret(next, n - 1));
        secret_chain
    }
}
