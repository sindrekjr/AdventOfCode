use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day09::solve_part_one(input),
        Part::P2 => Day09::solve_part_two(input),
    }
}

struct Day09;
impl Solution for Day09 {
    fn solve_part_one(input: String) -> String {
        let mut files: Vec<(u8, Option<u64>)> = vec![];

        let mut file_blocks: u64 = 0;
        let mut id: u64 = 0;
        for (i, ch) in input.char_indices() {
            let number = ch.to_digit(10).unwrap() as u8;

            if i % 2 == 0 {
                file_blocks += number as u64;
                files.push((number, Some(id)));
                id += 1;
            } else {
                files.push((number, None));
            }
        }

        let mut i = 0;
        let mut sum: u64 = 0;
        let mut b = 0;
        while b < file_blocks {
            let (blocks, id) = files[i];

            if blocks == 0 {
                i += 1;
                continue;
            }

            match id {
                None => {
                    let last_i = files.len() - 1;
                    let (trailing_blocks, id) = files[last_i];
                    match id {
                        None => {
                            files.remove(last_i);
                        }
                        Some(id) => {
                            if trailing_blocks <= blocks {
                                files[i].0 -= trailing_blocks;
                                files.remove(last_i);
                            } else {
                                files[last_i].0 -= blocks;
                                i += 1;
                            }

                            let blocks = blocks.min(trailing_blocks);

                            for _ in 0..blocks {
                                sum += b * id;
                                b += 1;
                            }
                        }
                    }
                }
                Some(id) => {
                    for _ in 0..blocks {
                        sum += b * id;
                        b += 1;
                    }

                    i += 1;
                }
            }
        }

        sum.to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut files: Vec<(u8, Option<u64>)> = vec![];

        let mut id: u64 = 0;
        for (i, ch) in input.char_indices() {
            let number = ch.to_digit(10).unwrap() as u8;

            if i % 2 == 0 {
                files.push((number, Some(id)));
                id += 1;
            } else {
                files.push((number, None));
            }
        }

        for (blocks, id) in files.iter().cloned().rev().collect::<Vec<_>>() {
            if let Some(id) = id {
                let (current_i, _) = files
                    .iter()
                    .enumerate()
                    .find(|&(_, &(_, matching_id))| matching_id == Some(id))
                    .unwrap();

                if let Some((empty_i, (empty_blocks, _))) = files
                    .iter()
                    .enumerate()
                    .find(|&(e_i, &(e_b, e_id))| e_i < current_i && e_id == None && e_b >= blocks)
                {
                    if *empty_blocks == blocks {
                        files[current_i] = (blocks, None);
                        files[empty_i] = (blocks, Some(id));
                    } else {
                        files[current_i] = (blocks, None);
                        files[empty_i].0 -= blocks;
                        files.insert(empty_i, (blocks, Some(id)));
                    }
                }
            }
        }

        let mut sum = 0;
        let mut b = 0;

        for (blocks, id) in files {
            match id {
                None => {
                    b += blocks as u64;
                }
                Some(id) => {
                    for _ in 0..blocks {
                        sum += b * id;
                        b += 1;
                    }
                }
            }
        }

        sum.to_string()
    }
}
