use crate::{
    core::{Part, Solution},
    utils::string::StrUtils,
};

use take_until::TakeUntilExt;

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day08::solve_part_one(input),
        Part::P2 => Day08::solve_part_two(input),
    }
}

struct Day08;
impl Solution for Day08 {
    fn solve_part_one(input: String) -> String {
        let grid: Vec<Vec<u8>> = input.lines().map(|line| line.digits()).collect();
        let height = grid.len();
        let length = grid[0].len();

        let mut visible = (height * 2) + (length * 2) - 4;
        for row in 1..length - 1 {
            let treeline = &grid[row];

            for col in 1..height - 1 {
                let tree = &treeline[col];

                if treeline[..col].iter().all(|t| t < tree)
                    || treeline[col + 1..].iter().all(|t| t < tree)
                    || grid[..row].iter().all(|r| &r[col] < tree)
                    || grid[row + 1..].iter().all(|r| &r[col] < tree)
                {
                    visible += 1;
                    continue;
                }
            }
        }

        visible.to_string()
    }

    fn solve_part_two(input: String) -> String {
        let grid: Vec<Vec<u8>> = input.lines().map(|line| line.digits()).collect();
        let height = grid.len();
        let length = grid[0].len();

        let mut score = 0;
        for row in 1..length - 1 {
            let treeline = &grid[row];

            for col in 1..height - 1 {
                let tree = &treeline[col];

                let consideration = treeline[..col]
                    .iter()
                    .rev()
                    .take_until(|t| t >= &tree)
                    .count()
                    * treeline[col + 1..]
                        .iter()
                        .take_until(|t| t >= &tree)
                        .count()
                    * grid[..row]
                        .iter()
                        .rev()
                        .take_until(|r| &r[col] >= tree)
                        .count()
                    * grid[row + 1..]
                        .iter()
                        .take_until(|r| &r[col] >= tree)
                        .count();

                if consideration > score {
                    score = consideration;
                }
            }
        }

        score.to_string()
    }
}
