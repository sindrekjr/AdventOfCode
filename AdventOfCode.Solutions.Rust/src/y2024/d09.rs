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
                                // println!("Sum is {}; add {} * {}", sum, b, id);

                                sum += b * id;
                                b += 1;
                            }
                        }
                    }
                }
                Some(id) => {
                    for _ in 0..blocks {
                        // println!("Sum is {}; add {} * {}", sum, b, id);

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
        // let mut files: Vec<(u8, Option<u64>)> = vec![];

        // let mut file_blocks: u64 = 0;
        // let mut id: u64 = 0;
        // for (i, ch) in input.char_indices() {
        //     let number = ch.to_digit(10).unwrap() as u8;

        //     if i % 2 == 0 {
        //         file_blocks += number as u64;
        //         files.push((number, Some(id)));
        //         id += 1;
        //     } else {
        //         files.push((number, None));
        //     }
        // }

        // loop {
        //     let last_file = files.iter().enumerate().rfind(|(_, (_, id))| *id != None);
        //     if let Some((i, (blocks, id))) = last_file {
        //         let id = id.unwrap();

        //         let leftmost_empty = files.iter().enumerate().find(|(_, (empty_blocks, maybe_empty))| *maybe_empty == None && empty_blocks >= blocks);
        //         if let Some((left_i, (left_b, _))) = leftmost_empty {
        //             files[left_i].0 -= blocks;
        //         }
        //     }
        // }

        // let mut i = 0;
        // let mut sum: u64 = 0;
        // let mut b = 0;

        // while b < file_blocks {
        //     let (blocks, id) = files[i];

        //     if blocks == 0 {
        //         i += 1;
        //         continue;
        //     }

        //     match id {
        //         None => {
        //             let possible_match = files.iter().enumerate().rfind(|(_, (t_blocks, id))| *t_blocks <= blocks && *id != None);

        //             if let Some((last_i, (trailing_blocks, id))) = possible_match {
        //                 let id = id.unwrap();
        //                 let trailing_blocks = trailing_blocks.to_owned();
        //                 if trailing_blocks == blocks {
        //                     i += 1;
        //                 } else {
        //                     files[i].0 -= trailing_blocks;
        //                 }

        //                 for _ in 0..trailing_blocks {
        //                     sum += b * id;
        //                     b += 1;
        //                 }

        //                 files.remove(last_i);
        //             } else {
        //                 // b += blocks as u64;
        //                 i += 1;
        //                 continue;
        //             }
        //         }
        //         Some(id) => {
        //             for _ in 0..blocks {
        //                 // println!("Sum is {}; add {} * {}", sum, b, id);

        //                 sum += b * id;
        //                 b += 1;
        //             }

        //             i += 1;
        //         }
        //     }
        // }

        // sum.to_string()

        String::new()
    }
}
