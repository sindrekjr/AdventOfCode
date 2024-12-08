use std::collections::HashSet;

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
        let lines: Vec<&str> = input.lines().collect();
        let h = lines.len() as isize;
        let w = lines[0].len() as isize;

        let nodes: HashSet<(char, usize, usize)> = lines
            .iter()
            .enumerate()
            .flat_map(|(y, line)| {
                line.char_indices().filter_map(move |(x, ch)| match ch {
                    '.' => None,
                    ch => Some((ch, x, y)),
                })
            })
            .collect();

        let antinodes: HashSet<(usize, usize)> = nodes
            .iter()
            .flat_map(|&(ch, x, y)| {
                let mut antinodes = vec![];

                for &(i_ch, i_x, i_y) in &nodes {
                    if i_ch != ch {
                        continue;
                    }

                    if i_x == x && i_y == y {
                        continue;
                    }

                    let diff_x = x as isize - i_x as isize;
                    let diff_y = y as isize - i_y as isize;
                    let new_x = x as isize + diff_x;
                    let new_y = y as isize + diff_y;

                    if new_x >= 0 && new_x < w && new_y >= 0 && new_y < h {
                        antinodes.push((new_x as usize, new_y as usize));
                    }
                }

                antinodes
            })
            .collect();

        antinodes.len().to_string()
    }

    fn solve_part_two(input: String) -> String {
        let lines: Vec<&str> = input.lines().collect();
        let h = lines.len() as isize;
        let w = lines[0].len() as isize;

        let nodes: HashSet<(char, usize, usize)> = lines
            .iter()
            .enumerate()
            .flat_map(|(y, line)| {
                line.char_indices().filter_map(move |(x, ch)| match ch {
                    '.' => None,
                    ch => Some((ch, x, y)),
                })
            })
            .collect();

        let antinodes: HashSet<(usize, usize)> = nodes
            .iter()
            .flat_map(|&(ch, x, y)| {
                let mut antinodes = vec![];

                for &(i_ch, i_x, i_y) in &nodes {
                    if i_ch != ch {
                        continue;
                    }

                    if i_x == x && i_y == y {
                        antinodes.push((x, y));
                        continue;
                    }

                    let diff_x = x as isize - i_x as isize;
                    let diff_y = y as isize - i_y as isize;

                    let mut i = 1;
                    loop {
                        let new_x = x as isize + (diff_x * i);
                        let new_y = y as isize + (diff_y * i);

                        if new_x >= 0 && new_x < w && new_y >= 0 && new_y < h {
                            antinodes.push((new_x as usize, new_y as usize));
                            i += 1;
                        } else {
                            break;
                        }
                    }

                }

                antinodes
            })
            .collect();

        antinodes.len().to_string()
    }
}
